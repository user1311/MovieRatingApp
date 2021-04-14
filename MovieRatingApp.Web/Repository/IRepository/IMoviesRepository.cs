using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRatingApp.Web.Models;

namespace MovieRatingApp.Web.Repository.IRepository
{
    public interface IMoviesRepository
    {
        Task<ServiceResponse<List<Movies>>> GetAllMoviesAsync(string url, string token);
    }
}
