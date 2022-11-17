using BreakfastBuffetAppV2.Data;
using BreakfastBuffetAppV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BreakfastBuffetAppV2.Pages.Reception
{
    public class BreakfastHistoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string DateNow { get; set; }
        public List<BreakfastCheckIn> CheckedIn { get; set; } = new List<BreakfastCheckIn>();
        public DisplayModel Display { get; set; }
        public class DisplayModel
        {
            public int RoomNumber { get; set; } = 0;
            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }

        public BreakfastHistoryModel(ApplicationDbContext context)
        {
            _context = context;
            Display = new DisplayModel();
            DateNow = DateTime.Now.Day + "/" + DateTime.Now.Month;
        }
        public async Task OnGetAsync()
        {
            // Load all checkins for today from database
            var dbBreakfastCheckIns = await _context.BreakfastCheckIns
                .Where(b => b.Date.Day == DateTime.Now.Day && b.Date.Month == DateTime.Now.Month)
                .ToListAsync();
            
            if (dbBreakfastCheckIns == null) { RedirectToPage("Error"); return; }

            CheckedIn = dbBreakfastCheckIns;
        }
    }
}
