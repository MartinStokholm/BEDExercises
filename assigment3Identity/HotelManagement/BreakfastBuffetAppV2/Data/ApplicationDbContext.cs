using BreakfastBuffetAppV2.Models;
using BreakfastBuffetAppV2.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffetAppV2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<ReceptionUser>? ReceptionUsers { get; set; }
        public DbSet<WaiterUser>? WaiterUsers { get; set; }
        public DbSet<KitchenUser>? KitchenUsers { get; set; }

        public DbSet<BreakfastCheckIn> BreakfastCheckIns { get; set; } = default!;
        public DbSet<BreakfastGuestsExpected> BreakfastGuestsExpecteds { get; set; } = default!;
    }
}