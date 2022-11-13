using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ModelManagementAPI.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Expense> Expenses { get; set; } = default!;

        public DbSet<Job> Jobs { get; set; } = default!;

        public DbSet<Model> Models { get; set; } = default!;
    }
}
