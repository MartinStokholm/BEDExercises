using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

using BreakfastBuffetAppV2.Models;
using BreakfastBuffetAppV2.Data;

using Microsoft.EntityFrameworkCore;
using BreakfastBuffetAppV2.Hub;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace BreakfastBuffetAppV2.Pages.Kitchen
{
    [Authorize("Kitchen")]
    public class BreakfastReportModel : PageModel
    {
       
        private readonly ApplicationDbContext _context;

        public int _adultsExpected;
        public int _childrenExpected;
        public int _totalExpected;
        public int _adultsCheckedIn;
        public int _childrenCheckedIn;

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Now;
        }

        public BreakfastReportModel(ApplicationDbContext context)
        {
            _context = context;
            Input = new InputModel();
        }
        
        public async Task OnGet()
        {
            var dbExpected = await _context.BreakfastGuestsExpecteds
                .Where(b => b.Date.Day == Input.Date.Day && b.Date.Month == Input.Date.Month)
                .ToListAsync();

            if (dbExpected == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbExpected)
            {
                _adultsExpected = item.Adults;
                _childrenExpected = item.Children;
                _totalExpected = _adultsExpected + _childrenExpected;
            }

            var dbCheckedIn = await _context.BreakfastCheckIns
               .Where(b => b.Date.Day == Input.Date.Day && b.Date.Month == Input.Date.Month)
               .ToListAsync();

            if (dbCheckedIn == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbCheckedIn)
            {
                _adultsCheckedIn += item.Adults;
                _childrenCheckedIn += item.Children;
            }
        }

        public async Task OnPost()
        {
            var dbExpected = await _context.BreakfastGuestsExpecteds
                .Where(b => b.Date.Day == Input.Date.Day && b.Date.Month == Input.Date.Month)
                .ToListAsync();
            
            if (dbExpected == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbExpected)
            {
                _adultsExpected = item.Adults;
                _childrenExpected = item.Children;
                _totalExpected = _adultsExpected + _childrenExpected;
            }

            var dbCheckedIn = await _context.BreakfastCheckIns
               .Where(b => b.Date.Day == Input.Date.Day && b.Date.Month == Input.Date.Month)
               .ToListAsync();

            if (dbCheckedIn == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbCheckedIn)
            {
                _adultsCheckedIn += item.Adults;
                _childrenCheckedIn += item.Children;
            }
        }
    }
}
