using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ModellingManagementAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Expense> Expenses { get; set; } = default!;

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Model> Models { get; set; }
    }
}
