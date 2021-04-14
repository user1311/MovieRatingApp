using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieRatingApp.API.Helpers;
using MovieRatingApp.API.Models;
using MovieRatingApp.API.Models.DTOs;
using MovieRatingApp.API.Repository.IRepository;

namespace MovieRatingApp.API.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public UserRepository(IOptions<AppSettings> appSettings, UserManager<User> userManager,
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _jwtSettings = jwtSettings.Value;
        }


        public async Task<bool> IsUniqueUser(string username)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            return userExists == null;
        }

        public async Task<ServiceResponse<string>> Authenticate(AuthModel creds)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            var user = await _userManager.FindByEmailAsync(creds.Username);

            if (user == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User doesn't exist";
                return serviceResponse;
            }

            var loginResult = await _userManager.CheckPasswordAsync(user, creds.Password);

            if (!loginResult)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid credentials.";
            }
            else
            {
                serviceResponse.Success = true;
                var roles = await _userManager.GetRolesAsync(user);
                serviceResponse.Data = GenerateJwt(user, roles);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> Register(AuthModel newUser)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            bool userIsUnique = await IsUniqueUser(newUser.Username);
            if (!userIsUnique)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User already exists";
                return serviceResponse;
            }

            var userRegisterResult = await _userManager.CreateAsync(new User(){Email = newUser.Username,UserName = newUser.Username}, newUser.Password);

            if (!userRegisterResult.Succeeded)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = userRegisterResult.Errors.FirstOrDefault().Description;
            }
            else
            {
                var registeredUser = await _userManager.FindByEmailAsync(newUser.Username);
                await _userManager.AddToRoleAsync(registeredUser, "User");
                serviceResponse.Success = true;
                serviceResponse.Message = "User successfully created";
            }

            return serviceResponse;
        }

        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
