using System.ComponentModel.DataAnnotations;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace HotelRestaurantAPI.Pages.Management
{
    [Authorize("ReceptionStaff")]
    public class ReceptionModel : PageModel
    {
        private DateTime _now = DateTime.Now;
        private int _day = DateTime.Now.Day;
        private int _month = DateTime.Now.Month;

        public IActionResult OnPostRedirect()
        {
            return RedirectToPage("DisplayBreakfastData");
        }
        [BindProperty]
        public static bool ReservationSucceeded { get; set; }

        
        public string? ReservationMessage { get; set; }
        
        private IReservationService _reservationService;
        private HotelDataContext _context;

        public ReceptionModel(IReservationService reservationService, HotelDataContext context)
        {
            _reservationService = reservationService;
            _context = context;
        }
        
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var d = Input.Date.Day;
            var m = Input.Date.Month;

            if (Input.AdultAmount < 1 && Input.ChildrenAmount < 1)
            {
                ReservationMessage = "Guest amount must be more than zero!";
                return Page();
            }

            bool success = await _reservationService.AddExpected(_context, d, m, Input.AdultAmount, Input.ChildrenAmount);
            if (success)
            {
                ReservationMessage = "Success!";
            }
            else
            {
                ReservationMessage = "Error saving data to the database!";
            }
            
            return Page();
        }

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = new DateTime(2022, 11, 14);

            public int AdultAmount { get; set; } = 0;
            public int ChildrenAmount { get; set; } = 0;
        }


    }
}
