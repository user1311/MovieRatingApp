using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Validations;
using MovieRatingApp.API.Helpers;
using MovieRatingApp.API.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace MovieRatingApp.API.Data
{
    public static class DbSeeder
    {
        /// <summary>
        /// Implemented Seeder to fetch movies from reelgood api and just manually mapped response to Movies and TVShows since we don't have a lot to do here,
        /// also added test users and role so I don't have to make separate controllers just for this
        /// </summary>
        /// <param name="dbContext"></param>
        public static void SeedDb(AppDbContext dbContext, UserManager<User> _userManager,RoleManager<IdentityRole> _roleManager)
        {
            SeedMovies(dbContext);

            SeedTVShows(dbContext);

            SeedRole(_roleManager);

            SeedUsers(_userManager);
        }

        private static void SeedMovies(AppDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Movies.Count() == 0)
            {
                var movies = ReelGoodAPIService.GetMovies();
                
                if (movies.results.Length > 0)
                {
                    var random = new Random();
                    foreach (var movie in movies.results)
                    {
                        MovieDetails movieDetails = ReelGoodAPIService.GetMovieDetails(movie.id);

                        var actors = string.Empty;

                        foreach (var actor in movieDetails.people)
                        {
                            actors += actor.name + ", ";
                        }

                        Movies newMovie = new Movies()
                        {
                            Id = movie.id,
                            Description = movie.overview,
                            ReleaseDate = movie.released_on,
                            Title = movie.title,
                            Actors = actors,
                            ImageURL = $"https://img.reelgood.com/content/movie/{movie.id}/poster-342.webp",
                            Rating = random.Next(1,6)
                        };

                        dbContext.Movies.Add(newMovie);
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        private static void SeedTVShows(AppDbContext dbContext)
        {
            if (dbContext.TvShows.Count() == 0)
            {
                var tvShows = ReelGoodAPIService.GetTvShows();

                if (tvShows.results.Length > 0)
                {
                    var random = new Random();

                    foreach (var tvShow in tvShows.results)
                    {
                        MovieDetails showDetails = ReelGoodAPIService.GetTvShowDetails(tvShow.id);

                        var actors = string.Empty;

                        foreach (var actor in showDetails.people)
                        {
                            actors += actor.name + ", ";
                        }

                        TVShows newShow = new TVShows()
                        {
                            Id = tvShow.id,
                            Description = tvShow.overview,
                            ReleaseDate = tvShow.released_on,
                            Title = tvShow.title,
                            Actors = actors,
                            ImageURL = $"https://img.reelgood.com/content/show/{tvShow.id}/poster-342.webp",
                            Rating = random.Next(1,6)
                        };

                        dbContext.TvShows.Add(newShow);
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        private static void SeedRole(RoleManager<IdentityRole> _roleManager)
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                var userRole = new IdentityRole("User");
                var role = _roleManager.CreateAsync(userRole).Result;

                if (role.Succeeded)
                {
                    return;
                }
            }
        }

        private static void SeedUsers(UserManager<User> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new User() {Email = "testTest1@test.com",UserName = "testTest1@test.com"};
                var created =_userManager.CreateAsync(user, "testTest_1234").Result;

                if (created.Succeeded)
                {
                    var createdUser = _userManager.FindByEmailAsync(user.Email).Result;
                    var added = _userManager.AddToRoleAsync(createdUser, "User").Result;

                    if (added.Succeeded)
                    {
                        return;
                    }
                }
            }
        }
    }
}
