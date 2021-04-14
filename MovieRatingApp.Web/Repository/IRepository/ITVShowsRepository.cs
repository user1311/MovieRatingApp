using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRatingApp.Web.Models;

namespace MovieRatingApp.Web.Repository.IRepository
{
    public interface ITVShowsRepository
    {
        Task<ServiceResponse<List<TVShows>>> GetAllShowsAsync(string url, string token);
    }
}
