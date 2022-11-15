using MessagePack;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace HotelRestaurantAPI.Models;


public class CheckedIn
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    [Required] public int Adults { get; set; } = 0;
    [Required] public int Children { get; set; } = 0;
    public DailyBreakfast DailyBreakfast { get; set; } = default!;
}