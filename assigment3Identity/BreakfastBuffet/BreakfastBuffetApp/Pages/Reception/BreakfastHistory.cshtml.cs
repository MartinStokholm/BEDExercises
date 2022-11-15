using BreakfastBuffetApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BreakfastBuffetApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffetApp.Pages.Reception
{
    [Authorize("Reception")]
    public class BreakfastHistoryModel : PageModel
    {
        private DateTime _now = DateTime.Now;
        private int _day = DateTime.Now.Day;
        private int _month = DateTime.Now.Month;

        public DisplayModel Display { get; set; }
        public class DisplayModel
        {
            public int RoomNumber { get; set; }
            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }

        public string DateNow { get; set; }
        private readonly ApplicationDbContext _context;

        public BreakfastHistoryModel(ApplicationDbContext context)
        {
            _context = context;
            DateNow = _day + "/" + _month;
        }

        public IList<CheckedIn> CheckedIn { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Load all checkins for today 
            var breakfastData = await _context.KitchenReports.FirstOrDefaultAsync(r => r.Day == _day && r.Month == _month);
            if (breakfastData != null)
            {
                CheckedIn = breakfastData.CheckedIn;
            }
            else
            {
                // Redirect to error page
                RedirectToPage("Error");
            }
        }

    }
}
