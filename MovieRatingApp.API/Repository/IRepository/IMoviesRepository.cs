using System.Collections.Generic;
using MovieRatingApp.API.Models;

namespace MovieRatingApp.API.Repository.IRepository
{
    public interface IMoviesRepository
    {
        ServiceResponse<List<Movies>> GetMovies(string query);
    }
}
