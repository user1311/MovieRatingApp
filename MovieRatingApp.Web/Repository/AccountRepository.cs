using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovieRatingApp.Web.Models;
using MovieRatingApp.Web.Models.DTOs;
using MovieRatingApp.Web.Repository.IRepository;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace MovieRatingApp.Web.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private RestClient client { get; set; }

        public AccountRepository()
        {
            client = new RestClient();
            client.UseNewtonsoftJson();
        }

        public async Task<ServiceResponse<string>> Login(string url, AuthDTO objUser)
        {
            var request = new RestRequest(url);
            
            if (objUser != null)
            {
                request.AddJsonBody(objUser);
            }
            else
            {
                return new ServiceResponse<string>()
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }

            var response = await client.PostAsync<ServiceResponse<string>>(request);

            if (response.Success)
            {
                return new ServiceResponse<string>()
                {
                    Data = response.Data,
                    Message = "",
                    Success = true
                };
            }
            else
            {
                return new ServiceResponse<string>()
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }

        }

        public async Task<ServiceResponse<string>> Register(string url, AuthDTO objUser)
        {
            var request = new RestRequest(url);

            if (objUser != null)
            {
                request.AddJsonBody(objUser);
            }
            else
            {
                return new ServiceResponse<string>()
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }

           
            var response = await client.PostAsync<ServiceResponse<string>>(request);

            if (response.Success)
            {
                return new ServiceResponse<string>()
                {
                    Success = true,
                    Message = ""
                };
            }
            else
            {
                return new ServiceResponse<string>()
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }
        }
    }
}
