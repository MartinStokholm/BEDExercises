using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<RestAPI.Models.Expense> Expense { get; set; } = default!;

        public DbSet<RestAPI.Models.Job> Job { get; set; }

        public DbSet<RestAPI.Models.Model> Model { get; set; }
    }
}
