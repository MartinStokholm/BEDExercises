using BreakfastBuffetApp.Data;
using BreakfastBuffetApp.Models;
using BreakfastBuffetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BreakfastBuffetApp.Pages.Waiter
{
    [Authorize("Waiter")]
    public class CheckInModel : PageModel
    {
        [BindProperty] public static bool CheckInSucceeded { get; set; }
        public string? CheckInMessage { get; set; }
        private IBreakfastService _breakfastService;
        private ApplicationDbContext _context;

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = new DateTime();
            public int Roomnumber { get; set; } = 0;
            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }

        public IActionResult OnPostRedirect()
        {
            return Page();
        }

        public CheckInModel(IBreakfastService breakfastService, ApplicationDbContext context)
        {
            _breakfastService = breakfastService;
            _context = context;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var d = Input.Date.Day;
            var m = Input.Date.Month;

            if (Input.Adults < 1 && Input.Children < 1 && Input.Roomnumber < 1)
            {
                CheckInMessage = "Number of guests and roomnumber must be bigger than zero!";
                return Page();
            }

            bool success = await _breakfastService.AddCheckedIn(_context, d, m, Input.Roomnumber, Input.Adults, Input.Children);
            if (success) { CheckInMessage = "Success!"; }
            else { CheckInMessage = "Error saving data to the database!"; }

            return Page();
        }
    }
}
