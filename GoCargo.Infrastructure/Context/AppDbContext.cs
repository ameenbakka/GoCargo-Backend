using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.IsBlocked)
                .HasDefaultValue("false");
            modelBuilder.Entity<User>()
                .HasMany(x => x.Bookings)
                .WithOne(r => r.User)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Booking>()
               .Property(b => b.Weight).
               HasPrecision(10, 2);
            modelBuilder.Entity<Booking>()
               .Property(b => b.EstimatedFare).
               HasPrecision(10, 2);
            modelBuilder.Entity<User>()
             .HasMany(x => x.Vehicles)
             .WithOne(r => r.User)
             .HasForeignKey(x => x.DriverId);
        }
    }
}
