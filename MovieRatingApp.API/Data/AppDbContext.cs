using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieRatingApp.API.Models;

namespace MovieRatingApp.API.Data
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Movies> Movies { get; set; }
        public DbSet<TVShows> TvShows { get; set; }

    }
}
