using BreakfastBuffetApp.Models;
using BreakfastBuffetApp.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffetApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KitchenReport>().HasKey(k => new { k.Day, k.Month });

            modelBuilder.Entity<KitchenReport>().HasMany(b => b.CheckedIn)
                .WithOne(c => c.KitchenReport);
            modelBuilder.Entity<KitchenReport>().HasOne(b => b.Expected)
                .WithOne(e => e.KitchenReport)
                .HasForeignKey<Expected>(e => new { e.Day, e.Month });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ReceptionUser> ReceptionUsers { get; set; }
        public DbSet<WaiterUser> WaiterUsers { get; set; }
        public DbSet<KitchenstaffUser> KitchenstaffUsers { get; set; }
        public DbSet<CheckedIn> CheckedIns { get; set; }
        public DbSet<KitchenReport> KitchenReports { get; set; }
    }
}