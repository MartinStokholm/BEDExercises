using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelRestaurantAPI.Pages.Management.Reception
{
    [Authorize("ReceptionStaff")]
    public class DisplayBreakfastDataModel : PageModel
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
        private readonly HotelRestaurantAPI.Data.HotelDataContext _context;

        public DisplayBreakfastDataModel(HotelRestaurantAPI.Data.HotelDataContext context)
        {
            _context = context;
            DateNow = _day + "/" + _month;
        }

        public IList<CheckedIn> CheckedIn { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Load all checkins for today 
            var breakfastData = await _context.DailyBreakfasts.FirstOrDefaultAsync(r => r.Day == _day && r.Month == _month);
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
