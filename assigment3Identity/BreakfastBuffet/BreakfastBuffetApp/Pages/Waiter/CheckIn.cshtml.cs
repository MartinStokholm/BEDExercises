using BreakfastBuffetApp.Data;
using BreakfastBuffetApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BreakfastBuffetApp.Pages.Waiter
{
    [Authorize("Waiter")]
    public class CheckInModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CheckInModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet() { return Page(); }

        [BindProperty]
        public CheckedIn CheckedIn { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("../Error");
            }

            _context.CheckedIns.Add(CheckedIn);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
