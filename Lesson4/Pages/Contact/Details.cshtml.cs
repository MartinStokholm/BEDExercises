using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lesson4.Pages;

public class DetailsModel : PageModel
{
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ILogger<DetailsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}