using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ModelManagementAPI.Pages
{
    public class ExpenseUpdateModel : PageModel
    {
        private readonly ILogger<ExpenseUpdateModel> _logger;

        public ExpenseUpdateModel(ILogger<ExpenseUpdateModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}