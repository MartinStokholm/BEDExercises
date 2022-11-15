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
        private DateTime _now = DateTime.Now;
        private int _day = DateTime.Now.Day;
        private int _month = DateTime.Now.Month;
        [BindProperty] public static bool ExpectedGuestsSucceeded { get; set; }
        public string? ExpectedGuestsMessage { get; set; }
        private IBreakfastService _breakfastService;
        private ApplicationDbContext _context;

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = new DateTime(2022, 11, 14);

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
