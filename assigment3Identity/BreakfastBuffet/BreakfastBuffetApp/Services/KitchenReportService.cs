
using Microsoft.AspNetCore.SignalR;
namespace BreakfastBuffetApp.Services
{
    public class KitchenReportService : Hub<IKitchenReportService>
    {
        public async Task KitchenUpdate()
        {
            await Clients.All.KitchenUpdate();
        }
    }
}
