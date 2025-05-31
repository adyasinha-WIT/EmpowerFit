using Microsoft.EntityFrameworkCore;
using ExFit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExFit.DataAcces.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<MembershipPlan> MembershipPlans { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<UserMembership> UserMemberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Membership Plans
            modelBuilder.Entity<MembershipPlan>().HasData(
                new MembershipPlan { Id = 1, Name = "1 Month", Price = 9.99M, DurationInDays = 30 },
                new MembershipPlan { Id = 2, Name = "3 Months", Price = 24.99M, DurationInDays = 90 },
                new MembershipPlan { Id = 3, Name = "6 Months", Price = 44.99M, DurationInDays = 180 },
                new MembershipPlan { Id = 4, Name = "1 Year", Price = 79.99M, DurationInDays = 365 }
            );
        }
    }
}
