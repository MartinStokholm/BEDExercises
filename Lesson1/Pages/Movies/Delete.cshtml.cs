using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lesson1.Models;

namespace Lesson1.Pages_Movies
{
    public class DeleteModel : PageModel
    {
        private readonly Lesson1Context _context;

        public DeleteModel(Lesson1Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (movie == null)
            {
                return NotFound();
            }
            else 
            {
                Movie = movie;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }
            var movie = await _context.Movie.FindAsync(id);

            if (movie != null)
            {
                Movie = movie;
                _context.Movie.Remove(Movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
