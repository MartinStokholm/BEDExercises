using ModelManagementAPI.Controllers;
using ModelManagementAPI.Entities;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ModelManagementAPI.Hubs

{
    [HubName("messageHub")]
    public class MessageHub : Hub
    {
        public MessageHub()
        {

        }
    }
}
