using System.Collections.Generic;
using MovieRatingApp.API.Models;

namespace MovieRatingApp.API.Repository.IRepository
{
    public interface ITVShowsRepository
    {
        ServiceResponse<List<TVShows>> GetShows(string query);
    }
}
