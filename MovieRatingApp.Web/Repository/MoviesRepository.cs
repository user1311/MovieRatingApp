using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRatingApp.Web.Models;
using MovieRatingApp.Web.Repository.IRepository;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace MovieRatingApp.Web.Repository
{
    public class MoviesRepository:IMoviesRepository
    {
        private RestClient client { get; set; }
        public MoviesRepository()
        {
            client = new RestClient();
            client.UseNewtonsoftJson();
        }

        public async Task<ServiceResponse<List<Movies>>> GetAllMoviesAsync(string url, string token)
        {
            var request = new RestRequest(url, Method.GET,DataFormat.Json);

            var response = await client.GetAsync<ServiceResponse<List<Movies>>>(request);

            if(response.Success)
                return response;
            return null;
        }
    }
}
