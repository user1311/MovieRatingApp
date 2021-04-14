using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRatingApp.API.Models;
using MovieRatingApp.API.Repository.IRepository;

namespace MovieRatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(ServiceResponse<List<Movies>>))]
        [ProducesResponseType(400)]
        public IActionResult GetMovies([FromQuery] string query)
        {
            var response = _moviesRepository.GetMovies(query);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
