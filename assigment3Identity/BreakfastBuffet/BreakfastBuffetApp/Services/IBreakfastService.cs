using BreakfastBuffetApp.Data;

namespace BreakfastBuffetApp.Services
{
    public interface IBreakfastService
    {
        Task<bool> AddCheckedIn(ApplicationDbContext context, int day, int month, int roomNumber, int adults, int children);
        Task<bool> AddExpected(ApplicationDbContext context, int day, int month, int adults, int children);
    }
}