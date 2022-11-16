using BreakfastBuffetApp.Data;
using BreakfastBuffetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffetApp.Services
{
    public class BreakfastService : IBreakfastService
    {
        public async Task<bool> AddExpected(ApplicationDbContext context, int day, int month, int adults, int children)
        {
            var dbKitchenReports = await context.KitchenReports
                .Include(b => b.Expected)
                .FirstOrDefaultAsync(b => b.Day == day && b.Month == month);
            
            if (dbKitchenReports == null)
            {
                List<KitchenReport> kitchenReport = new();
                for (int i = day; i < day + 7; i++)
                {
                    var kp = new KitchenReport() { Day = i, Month = 11 };
                    kitchenReport.Add(kp);
                }

                foreach (var item in kitchenReport)
                {
                    context.KitchenReports.Add(item);
                    context.SaveChanges();
                }
            }

            dbKitchenReports.Expected.Adults += adults;
            dbKitchenReports.Expected.Children += children;

            context.Entry(dbKitchenReports).State = EntityState.Modified;

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
            var dbKitchenReports = context.KitchenReports
                .Include(b => b.CheckedIn)
                .FirstOrDefault(b => b.Day == day && b.Month == month);

            if (dbKitchenReports == null)
            {
                return false;
            }

            dbKitchenReports.CheckedIn.Add(new CheckedIn
            {
                RoomNumber = roomNumber,
                Adults = adults,
                Children = children
            });

            context.Entry(dbKitchenReports).State = EntityState.Modified;

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
