using BreakfastBuffetAppV2.Data;
using BreakfastBuffetAppV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BreakfastBuffetAppV2.Pages.Reception
{
    public class ExpectedGuestsModel : PageModel
    {
        private ApplicationDbContext _context;
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Today;

            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }

        public ExpectedGuestsModel(ApplicationDbContext context)
        {
            _context = context;
            Input = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           var breakfastGuestsExpected = new BreakfastGuestsExpected
           {
                Adults = Input.Adults,
                Children = Input.Children,
                Date = Input.Date
           };

            _context.BreakfastGuestsExpecteds.Add(breakfastGuestsExpected);
            await _context.SaveChangesAsync();

            return Page();
        }

        public void OnGet() {}
    }
}
