using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmaAPI.Models;

namespace PharmaAPI.Services
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
        new IdentityRole
        {
            Id = "1",  
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
        new IdentityRole
        {
            Id = "2", 
            Name = "Doctor",
            NormalizedName = "DOCTOR"
        }
    );
        }
    }
}
