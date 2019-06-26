using System;
using System.Collections.Generic;
using System.Text;
using TimeServed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TimeServed.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet <UserType> UserTypes { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<FavoriteLocation> FavoriteLocations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserType>().HasData(
                new UserType()
                {
                    Id = 1,
                    Role = "Attorney"
                },
                new UserType()
                {
                    Id = 2,
                    Role = "Guard"
                },
                new UserType()
                {
                    Id = 3,
                    Role = "Auditor"
                }
            );
            //create some locations
            modelBuilder.Entity<Location>().HasData(
              new Location()
              {
                  Id = 1,
                  LocationName = "South Central",
                  StreetAddress = "1001 Centre Way, Charleston, WV 25309"
              },
              new Location()
              {
                  Id = 2,
                  LocationName = "West Virginia Regional Jail and Correctional Facility",
                  StreetAddress = "1325 Virginia St E, Charleston, WV 25301"
              },
              new Location()
              {
                  Id = 3,
                  LocationName = "Mt Olive Correctional Complex",
                  StreetAddress = "1 Mountainside Way, Mt Olive, WV 25185"
              }
          );

            // Create some clients
            modelBuilder.Entity<Client>().HasData(
                new Client()
                {
                    Id = 1,
                    FirstName = "Jakob",
                    LastName = "Wildman",
                    LocationId =  3
                   
                },
                new Client()
                {
                    Id = 2,
                    FirstName = "Susan",
                    LastName = "MacAfee",
                    LocationId = 1
                }
            );
        }
    }
}
