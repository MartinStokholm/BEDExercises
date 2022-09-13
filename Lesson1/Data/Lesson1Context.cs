using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lesson1.Models;

    public class Lesson1Context : DbContext
    {
        public Lesson1Context (DbContextOptions<Lesson1Context> options)
            : base(options)
        {
        }

        public DbSet<Lesson1.Models.Movie> Movie { get; set; } = default!;
    }
