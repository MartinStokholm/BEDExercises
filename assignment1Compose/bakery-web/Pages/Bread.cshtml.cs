using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bakery_data.Models;

namespace MyApp.Namespace
{
    public class BreadModel : PageModel
    {
        public IList<Bread>? Breads { get; set; } = new List<Bread>();
        public async Task<IActionResult> OnGetAsync()
        {
            // Don't do this! We'll get back to it in Lesson 13
            // Leave me alone! I'm not made of time!
            using(HttpClient client = new HttpClient()) {
                var url  = Environment.GetEnvironmentVariable("API_URL");
                if(url is not null) {
                    client.BaseAddress = new Uri(url);                
                }
                Breads = await client.GetFromJsonAsync<IList<Bread>>("breads");
            }
            return Page();
        }
    }
}
