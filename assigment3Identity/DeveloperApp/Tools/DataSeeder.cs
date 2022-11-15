using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperApp.Tools;

public class DataSeeder
{
    private HotelDataContext _context;
    public DataSeeder(HotelDataContext context)
    {
        _context = context;
    }
    /*
     * All seeding functions go into this class
     */
    public void SeedReservations(int startDay)
    {
        List<DailyBreakfast> reservations = new();
        for (int i = startDay; i < startDay + 7; i++)
        {
            var b = new DailyBreakfast() { Day = i, Month = 11 };
            reservations.Add(b);
        }

        foreach (var b in reservations)
        {
            _context.DailyBreakfasts.Add(b);
            _context.SaveChanges();
        }
    }
    
}