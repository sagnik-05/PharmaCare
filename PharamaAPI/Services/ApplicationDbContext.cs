using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmaAPI.Models;
using System;

namespace PharmaAPI.Services
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding roles with GUIDs
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
            builder.Entity<Sales>()
            .HasOne(s => s.Drug)
            .WithMany()
            .HasForeignKey(s => s.DrugId);

        }
    }
}
