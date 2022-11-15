// See https://aka.ms/new-console-template for more information

using System.Configuration;
using DeveloperApp.Tools;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


Console.WriteLine("Hello, World!");

DbManager manager = new();

Console.WriteLine("Database loaded");

bool done = false;
while (!done)
{
    string input = Console.ReadLine();
    
    switch(input)
    {
        case "quit":
        {
            done = true;
            break;
        }
        case "seed":
        {
            Console.WriteLine("Seeding data.");
            try
            {
                manager.Seeder.SeedReservations(14);
            }
            catch(Exception ex)
            {
                Console.WriteLine("nuh-uh");
                Console.WriteLine(ex.ToString());
            }
            break;
        }
        case "reset":
        {
            Console.WriteLine("Resetting HotelDbContext");
            manager.ResetHotelDb();
            break;
        }
       
        default:
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            break;
        }
    }
}