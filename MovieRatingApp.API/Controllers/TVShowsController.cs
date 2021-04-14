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
    public class TVShowsController : ControllerBase
    {
        private ITVShowsRepository _tvShowsRepository;

        public TVShowsController(ITVShowsRepository tvShowsRepository)
        {
            _tvShowsRepository = tvShowsRepository;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(ServiceResponse<List<Movies>>))]
        [ProducesResponseType(400)]
        public IActionResult GetMovies([FromQuery] string query)
        {
            var response = _tvShowsRepository.GetShows(query);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
