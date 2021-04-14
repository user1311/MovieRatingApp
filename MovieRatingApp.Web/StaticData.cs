using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatingApp.Web
{
    public static class StaticData
    {
        public static string APIBaseUrl = "https://localhost:44347/";
        public static string MoviesUrl = APIBaseUrl + "api/movies";
        public static string TvShowsUrl = APIBaseUrl + "api/tvshows";
        public static string AccountApiPath= APIBaseUrl + "api/Users/";

    }
}
