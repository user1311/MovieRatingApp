using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRatingApp.API.Models;
using MovieRatingApp.API.Models.DTOs;

namespace MovieRatingApp.API.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUser(string username);
        Task<ServiceResponse<string>> Authenticate(AuthModel creds);
        Task<ServiceResponse<string>> Register(AuthModel newUser);
    }
}
