using BreakfastBuffetAppV2.Data;
using BreakfastBuffetAppV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BreakfastBuffetAppV2.Pages.Waiter
{
    public class CheckinGuestsModel : PageModel
    {
        private ApplicationDbContext _context;
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Today;

            public int Roomnumber { get; set; }
            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }

        public CheckinGuestsModel(ApplicationDbContext context)
        {
            _context = context;
            Input = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var breakfastCheckin = new BreakfastCheckIn
            {
                Adults = Input.Adults,
                Kids = Input.Children,
                Date = Input.Date,
                Roomnumber = Input.Roomnumber
            };

            _context.BreakfastCheckIns.Add(breakfastCheckin);
            await _context.SaveChangesAsync();

            return Page();
        }

        public void OnGet() { }
    }
}

