using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BreakfastBuffetApp.Pages
{

    [Authorize("Reception")]
    public class ReceptionModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
