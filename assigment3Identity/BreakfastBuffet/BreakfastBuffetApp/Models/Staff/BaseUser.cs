using Microsoft.AspNetCore.Identity;

namespace BreakfastBuffetApp.Models.Staff;

public class BaseUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}