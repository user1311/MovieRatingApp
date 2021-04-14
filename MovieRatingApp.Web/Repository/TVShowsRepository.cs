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
    public class TVShowsRepository:ITVShowsRepository
    {
        private RestClient client { get; set; }
        public TVShowsRepository()
        {
            client = new RestClient();
            client.UseNewtonsoftJson();
        }

        public async Task<ServiceResponse<List<TVShows>>> GetAllShowsAsync(string url, string token)
        {
            var request = new RestRequest(url, Method.GET,DataFormat.Json);

            var response = await client.GetAsync<ServiceResponse<List<TVShows>>>(request);

            if(response.Success)
                return response;
            return null;
        }
    }
}
