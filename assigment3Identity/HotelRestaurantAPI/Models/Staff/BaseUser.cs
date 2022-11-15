using Microsoft.AspNetCore.Identity;

namespace HotelRestaurantAPI.Models.Staff;

public class BaseUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}