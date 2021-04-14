using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MovieRatingApp.API.Helpers;
using MovieRatingApp.API.Models.DTOs;
using MovieRatingApp.API.Repository.IRepository;

namespace MovieRatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public UsersController(IUserRepository userRepository, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] AuthModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.Authenticate(model);

            if (!user.Success)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AuthModel newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userServiceResponse = await _userRepository.Register(newUser);

            if (!userServiceResponse.Success)
            {
                return BadRequest(userServiceResponse);
            }

            return Ok(userServiceResponse);
        }
    }
}
