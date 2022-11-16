using BreakfastBuffetApp.Models;
using BreakfastBuffetApp.Data;
using BreakfastBuffetApp.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace BreakfastBuffetApp.Pages.Kitchen
{
    public class KitchenReportModel : PageModel
    {
        private DateTime _now = DateTime.Now;
        private int _day = DateTime.Now.Day;
        private int _month = DateTime.Now.Month;

        private readonly IHubContext<KitchenReportService, IKitchenReportService> _kitchenContext;
        private readonly ApplicationDbContext _context;

        public int _adultsExpected;
        public int _childrenExpected;
        public int _totalExpected;
        public int _adultsCheckedIn;
        public int _childrenCheckedIn;

        public KitchenReportModel(ApplicationDbContext context,
            IHubContext<KitchenReportService, IKitchenReportService> kitchenContext)
        {
            _context = context;
            _kitchenContext = kitchenContext;
        }

        private DateTime _today = DateTime.Now;
        
        public async Task OnGet()
        {
            var myExpected = await GetExpected(_today);

            if (myExpected != null)
            {
                _adultsExpected = myExpected.Adults;
                _childrenExpected = myExpected.Children;
                _totalExpected = _adultsExpected + _childrenExpected;
            }
            else
            {
                ModelState.AddModelError("Input.Date", "No quest on this date");
            }

            var myKitchenReport = await GetKitchenReport(DateTime.Now);
            
            if (myKitchenReport != null)
            {
                foreach (var checkedIn in myKitchenReport.CheckedIn)
                {
                    _adultsCheckedIn += checkedIn.Adults;
                    _childrenCheckedIn += checkedIn.Children;
                }
            }
            else
            {
                ModelState.AddModelError("Input.Date", "No guest is checked in on this date");
            }
        }

        public async Task OnPost()
        {
            var myExpected = await GetExpected(Input.Date);
            if (myExpected != null)
            {
                _adultsExpected = myExpected.Adults;
                _childrenExpected = myExpected.Children;
                _totalExpected = _adultsExpected + _childrenExpected;
            }
            else
            {
                ModelState.AddModelError("Input.Date", "No quest on this date");
            }
            
            var myKitchenReport = await GetKitchenReport(DateTime.Now);
            
            if (myKitchenReport != null)
            {
                foreach (var checkedIn in myKitchenReport.CheckedIn)
                {
                    _adultsCheckedIn += checkedIn.Adults;
                    _childrenCheckedIn += checkedIn.Children;
                }
            }
            else
            {
                ModelState.AddModelError("Input.Date", "No guest is checked in on this date");
            }
        }

        private async Task<Expected?> GetExpected(DateTime date)
        {
            var kitchenReports = await _context.KitchenReports
                .Include(b => b.Expected)
                .Where(p => p.Day == date.Day)
                .FirstOrDefaultAsync();
            
            return kitchenReports?.Expected;
        }

        private async Task<KitchenReport?> GetKitchenReport(DateTime date)
        {
            return await _context.KitchenReports
                .Where(p => p.Day == date.Day)
                .Include(x => x.CheckedIn)
                .FirstOrDefaultAsync();
        }

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Now;
        }
    }
}
