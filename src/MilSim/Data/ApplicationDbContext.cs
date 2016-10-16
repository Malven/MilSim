using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MilSim.Models;

namespace MilSim.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Fireteam> Fireteams { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //one-to-many 

            builder.Entity<Fireteam>()
                        .HasOne( s => s.Operation ) // Student entity requires Standard 
                        .WithMany( s => s.Fireteams ); // Standard entity includes many Students entities
        }
    }
}
