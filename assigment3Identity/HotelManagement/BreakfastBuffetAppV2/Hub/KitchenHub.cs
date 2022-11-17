using Microsoft.AspNetCore.SignalR;
namespace BreakfastBuffetAppV2.Hub
{
    public class KitchenHub : Hub<IKitchenHub>
    {
        public async Task KitchenUpdate()
        {
            await Clients.All.KitchenUpdate();
        }
    }
}
