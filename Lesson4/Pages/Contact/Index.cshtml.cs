using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lesson4.Services;

namespace Lesson4.Pages;

public class ContactIndexModel : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; }

    public StoreContactInfo Service { get; set; }

    // Constructor with dependency injection from service
    public ContactIndexModel(StoreContactInfo service)
    {
        Service = service;
        Input = new InputModel{}; 
    }

    public void OnGet(){}
    
    public IActionResult OneDetails(string contact)
    {
        return RedirectToPage("Contact/Details", new { Contact = contact });
    }

    // This method is called when the form is submitted
    public IActionResult onPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Service.Contacts.Add(new Models.Contact
        {
            Name = Input.Name,
            Email = Input.Email,
            Phone = Input.Phone
        });
        return RedirectToPage("Index");
    }

    // This is the model that will be bound to the form 
    public class InputModel
    {
        [Required]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [Phone]
        public string Phone { get; set; } = "";
    }

}