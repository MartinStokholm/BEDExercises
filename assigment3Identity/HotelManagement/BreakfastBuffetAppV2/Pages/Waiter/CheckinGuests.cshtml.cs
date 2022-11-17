using BreakfastBuffetAppV2.Data;
using BreakfastBuffetAppV2.Hub;
using BreakfastBuffetAppV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace BreakfastBuffetAppV2.Pages.Waiter
{
    [Authorize("Waiter")]
    public class CheckinGuestsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<KitchenHub, IKitchenHub> _kitchenHub;

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

        public CheckinGuestsModel(ApplicationDbContext context, IHubContext<KitchenHub, IKitchenHub> kitchenHub)
        {
            _context = context;
            Input = new InputModel();
            _kitchenHub = kitchenHub;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var breakfastCheckin = new BreakfastCheckIn
            {
                Adults = Input.Adults,
                Children = Input.Children,
                Date = Input.Date,
                Roomnumber = Input.Roomnumber
            };

            _context.BreakfastCheckIns.Add(breakfastCheckin);
            await _context.SaveChangesAsync();
            _kitchenHub.Clients.All.KitchenUpdate();
            return Page();
        }

        public void OnGet() { }
    }
}

