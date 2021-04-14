using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatingApp.API.Models.DTOs
{
    public class RateMovieDTO
    {
        public string MovieId { get; set; }
        public double Rating { get; set; }
    }
}
