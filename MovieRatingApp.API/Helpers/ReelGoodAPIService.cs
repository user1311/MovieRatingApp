using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace MovieRatingApp.API.Helpers
{
    public static class ReelGoodAPIService
    {
        public static string BaseURL =
            "https://api.reelgood.com/v3.0/content/browse/filtered?availability=onAnySource&hide_seen=false&hide_tracked=false&hide_watchlisted=false&imdb_end=10&imdb_start=0&region=us&rg_end=100&rg_start=0&skip=0&sort=0&take=50&year_end=2021&year_start=1900";

        private static RestClient client = new RestClient(BaseURL);

        public static Shows GetMovies()
        {
            client.UseNewtonsoftJson();
            var request = new RestRequest("", Method.GET,DataFormat.Json);
            request.AddQueryParameter("content_kind", "movie");

            var movies = client.Get<Shows>(request);
            return movies.Data;
        }

        public static Shows GetTvShows()
        {
            client.UseNewtonsoftJson();
            var request = new RestRequest("", Method.GET);
            request.AddQueryParameter("content_kind", "show");
            var tvShows = client.Get<Shows>(request);
            return tvShows.Data;
        }

        public static MovieDetails GetMovieDetails(string movieId)
        {
            var client = new RestClient("https://api.reelgood.com/v3.0/content/movie/");
            client.UseNewtonsoftJson();
            
            var request = new RestRequest(movieId, Method.GET);
            var movieDetails = client.Get<MovieDetails>(request);
            return movieDetails.Data;
        }

        public static MovieDetails GetTvShowDetails(string tvShowId)
        {
            var client = new RestClient("https://api.reelgood.com/v3.0/content/show/");
            client.UseNewtonsoftJson();

            var request = new RestRequest(tvShowId, Method.GET);
            var tvShowDetails = client.Get<MovieDetails>(request);
            return tvShowDetails.Data;
        }

    }
}
