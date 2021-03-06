using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRatingApp.API.Data;
using MovieRatingApp.API.Models;
using MovieRatingApp.API.Repository.IRepository;

namespace MovieRatingApp.API.Repository
{
    public class TVShowsRepository:ITVShowsRepository
    {
        private AppDbContext _context;

        public TVShowsRepository(AppDbContext context)
        {
            _context = context;
        }
        public ServiceResponse<List<TVShows>> GetShows(string queryString)
        {
            ServiceResponse<List<TVShows>> response = new ServiceResponse<List<TVShows>>();

            var query = _context.TvShows.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryString))
            {

                var movies = query.Where(m =>
                    m.Title.Contains(queryString) || m.Description.Contains(queryString) ||
                    m.Actors.Contains(queryString)).OrderByDescending(m => m.Rating).ToList();

                if (movies.Count > 0)
                {
                    response.Data = movies;
                    response.Message = "";
                    response.Success = true;
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Message = "Search invalid";
                    response.Success = false;
                    return response;
                }
                
            }
            else
            {
                response.Message = "";
                response.Success = true;
                response.Data = query.ToList();
                return response;
            }
        }

    }
}
