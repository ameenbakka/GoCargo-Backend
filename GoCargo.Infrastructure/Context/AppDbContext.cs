using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using GoCargo.Domain.Models;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<DriverAssignment> driverAssignments { get; set; }
        public DbSet<DriverRequest> driverRequests { get; set; }
        public DbSet<Notification> Notifications { get; set; }



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
            modelBuilder.Entity<DriverRequest>()
                .HasOne(dr => dr.User)
                .WithOne(u => u.DriverRequest)
                .HasForeignKey<DriverRequest>(dr => dr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DriverAssignment>(entity =>
            {
                entity.HasOne(da => da.Booking)
                .WithOne(b => b.DriverAssignment)
                .HasForeignKey<DriverAssignment>(da => da.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(da => da.User)
                 .WithMany(d => d.DriverAssignments)
                 .HasForeignKey(da => da.DriverId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<DriverAssignment>()
               .HasKey(da => da.AssignmentId);
        }
    }
}
