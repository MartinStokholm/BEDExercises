using Microsoft.EntityFrameworkCore;

namespace HotelRestaurantAPI.Models;

public class Expected
{
    public int Id { get; set; }
    public int Adults { get; set; } = 0;
    public int Children { get; set; } = 0;
    public int Day { get; set; }
    public int Month { get; set; }
    public DailyBreakfast DailyBreakfast { get; set; } = default!;
}