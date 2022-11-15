using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelRestaurantAPI.Models;

public class DailyBreakfast
{
    public int Day { get; set; }
    public int Month { get; set; }
    public Expected Expected { get; set; } = new Expected()
            {Adults = 0, Children = 0};
    public List<CheckedIn> CheckedIn { get; set; } = new();

}