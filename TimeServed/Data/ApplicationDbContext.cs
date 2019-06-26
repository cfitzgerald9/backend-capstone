using System;
using System.Collections.Generic;
using System.Text;
using TimeServed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<UserClient> UserClients { get; set; }

        public DbSet<FavoriteLocation> FavoriteLocations { get; set; }


    }
}
