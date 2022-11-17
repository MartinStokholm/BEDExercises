using Microsoft.AspNetCore.SignalR;
namespace BreakfastBuffetAppV2.Hub
{
    public class KitchenReport : Hub<IKitchenReport>
    {
        public async Task KitchenUpdate()
        {
            await Clients.All.KitchenUpdate();
        }
    }
}
