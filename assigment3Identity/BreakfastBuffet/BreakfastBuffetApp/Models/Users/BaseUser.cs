using Microsoft.AspNetCore.Identity;

namespace BreakfastBuffetApp.Models.Users;

public class BaseUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}