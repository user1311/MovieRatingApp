﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatingApp.Web.Models
{
    public class TVShows
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Actors { get; set; }
        public string ImageURL { get; set; }
        public double Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
