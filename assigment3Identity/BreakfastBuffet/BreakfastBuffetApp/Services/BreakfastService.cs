using BreakfastBuffetApp.Data;
using BreakfastBuffetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffetApp.Services
{
    public class BreakfastService : IBreakfastService
    {
        public async Task<bool> AddExpected(ApplicationDbContext context, int day, int month, int adults, int children)
        {
            var b = await context.KitchenReports
                .Include(b => b.Expected)
                .FirstOrDefaultAsync(b => b.Day == day && b.Month == month);
            if (b == null)
            {
                return false;
            }

            b.Expected.Adults += adults;
            b.Expected.Children += children;

            context.Entry(b).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddCheckedIn(ApplicationDbContext context, int day, int month, int roomNumber, int adults, int children)
        {
            var b = context.KitchenReports
                .Include(b => b.CheckedIn)
                .FirstOrDefault(b => b.Day == day && b.Month == month);

            if (b == null)
            {
                return false;
            }

            b.CheckedIn.Add(new CheckedIn
            {
                RoomNumber = roomNumber,
                Adults = adults,
                Children = children
            });

            context.Entry(b).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

    }
}
