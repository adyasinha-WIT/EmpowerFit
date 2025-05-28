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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }

        public DbSet<MembershipPlan> MembershipPlans { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

    }
}