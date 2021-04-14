using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRatingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MovieRatingApp.Web.Models.DTOs;
using MovieRatingApp.Web.Repository.IRepository;

namespace MovieRatingApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAccountRepository _accountRepository;
        private IMoviesRepository _moviesRepository;
        private ITVShowsRepository _tvShowsRepository;


        public HomeController(ILogger<HomeController> logger, IAccountRepository accountRepository,IMoviesRepository moviesRepository,ITVShowsRepository tvShowsRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _moviesRepository = moviesRepository;
            _tvShowsRepository = tvShowsRepository;
        }

        public IActionResult Index()
        {

            return View();
        }

        public  IActionResult GetAllMovies()
        {
            return Json(new {data = _moviesRepository.GetAllMoviesAsync(StaticData.MoviesUrl,"").Result.Data});
        }

        public IActionResult GetAllShows()
        {
            return Json(new { data = _tvShowsRepository.GetAllShowsAsync(StaticData.TvShowsUrl, "").Result.Data });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            AuthDTO user = new AuthDTO();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] AuthDTO obj)
        {
            ServiceResponse<string> response = await _accountRepository.Login(StaticData.AccountApiPath + "authenticate", obj);
            if (response.Data == null)
                return View();

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, obj.Username));
            //identity.AddClaim(new Claim(ClaimTypes.Role,objUser.Role));

            var principle = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);

            HttpContext.Session.SetString("JWToken", response.Data);
            TempData["alert"] = $"Welcome {HttpContext.User.Identity.Name}";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Register()
        {
            AuthDTO user = new AuthDTO();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] AuthDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            var created = await _accountRepository.Register(StaticData.AccountApiPath + "register", obj);
            if (!created.Success)
                return View();

            TempData["alert"] = $"Welcome {obj.Username}";

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
