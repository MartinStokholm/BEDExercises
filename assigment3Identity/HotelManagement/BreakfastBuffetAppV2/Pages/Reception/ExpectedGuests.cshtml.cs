using BreakfastBuffetAppV2.Data;
using BreakfastBuffetAppV2.Hub;
using BreakfastBuffetAppV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace BreakfastBuffetAppV2.Pages.Reception
{
    [Authorize("Reception")]
    public class ExpectedGuestsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<KitchenHub, IKitchenHub> _kitchenHub;

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Today;

            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }

        public ExpectedGuestsModel(ApplicationDbContext context, IHubContext<KitchenHub, IKitchenHub> kitchenHub)
        {
            _context = context;
            _kitchenHub = kitchenHub;
            Input = new InputModel();
        }
        public IActionResult OnPostRedirect()
        {
            return RedirectToPage("BreakfastHistory");
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
            _kitchenHub.Clients.All.KitchenUpdate();
            return Page();
        }

        public void OnGet() {}
    }
}
