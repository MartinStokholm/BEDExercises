using BreakfastBuffetApp.Data;
using BreakfastBuffetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BreakfastBuffetApp.Pages
{

    [Authorize("Reception")]
    public class ReceptionModel : PageModel
    {
        [BindProperty] public static bool ExpectedGuestsSucceeded { get; set; }
        public string? ExpectedGuestsMessage { get; set; }
        private IBreakfastService _breakfastService;
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

        public IActionResult OnPostRedirect()
        {
            return RedirectToPage("BreakfastHistory");
        }

        public ReceptionModel(IBreakfastService breakfastService, ApplicationDbContext context)
        {
            _breakfastService = breakfastService;
            _context = context;
            Input = new InputModel();
        }

        public void OnGet() {}

        public async Task<IActionResult> OnPostAsync()
        {
            var d = Input.Date.Day;
            var m = Input.Date.Month;

            if (Input.Adults < 1 || Input.Children < 1)
            {
                ExpectedGuestsMessage = "Number of guests must be bigger than zero!";
                return Page();
            }

            bool success = await _breakfastService.AddExpected(_context, d, m, Input.Adults, Input.Children);
            if (success) { ExpectedGuestsMessage = "Success!"; }
            else { ExpectedGuestsMessage = "Error saving data to the database!"; }

            return Page();
        }

    }
}
