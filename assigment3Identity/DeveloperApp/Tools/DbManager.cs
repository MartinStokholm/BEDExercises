using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;

namespace DeveloperApp.Tools;

public class DbManager
{
    HotelDataContext _hotelContext;
    public HotelDataContext Context
    {
        get => _hotelContext;
    }
    public DataSeeder _seeder;
    public DataSeeder Seeder
    {
        get => _seeder;
    }

    public DbManager()
    {
        _hotelContext = SetupHotelContext();
        _seeder = new(_hotelContext);

    }

    public bool ResetHotelDb()
    {
        var breakfasts = _hotelContext.DailyBreakfasts.ToList();
        
        try
        {
            foreach (var b in breakfasts)
            {
                _hotelContext.DailyBreakfasts.Remove(b);
                _hotelContext.SaveChanges();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

    public T Get<T>(object[] key) where T : class
    {
        var result = _hotelContext.Find<T>(key);
        return result;
    }


    public HotelDataContext SetupHotelContext()
    {
        var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false);
        var c = configBuilder.Build();
        var hotelConnString = c.GetConnectionString("hotelDbConnection");
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseSqlServer(hotelConnString);
        var context = new HotelDataContext(optionsBuilder.Options);
        return context;
    }
}