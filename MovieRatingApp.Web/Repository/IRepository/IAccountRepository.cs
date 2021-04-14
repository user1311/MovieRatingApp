using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRatingApp.Web.Models;
using MovieRatingApp.Web.Models.DTOs;

namespace MovieRatingApp.Web.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task<ServiceResponse<string>> Login(string url, AuthDTO objUser);
        Task<ServiceResponse<string>> Register(string url, AuthDTO objUser);
    }
}
