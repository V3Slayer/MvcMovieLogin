using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcMovieLogin.Models;

namespace MvcMovieLogin.Models
{
    public class MvcMovieContext : IdentityDbContext<ApplicationUser>
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<MvcMovieLogin.Models.Movie> Movie { get; set; }

        public DbSet<MvcMovieLogin.Models.Review> Review { get; set; }

        //public DbSet<MvcMovieLogin.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
