using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BreakfastBuffetApp.Pages
{

    [Authorize("Waiter")]
    public class WaiterModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
