using System.Security.Cryptography.X509Certificates;
using HotelRestaurantAPI.Models.Staff;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelRestaurantAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ReceptionUser> ReceptionUsers { get; set; }
        public DbSet<WaiterUser> WaiterUsers { get; set; }
        public DbSet<KitchenstaffUser> KitchenstaffUsers { get; set; }
    }
}