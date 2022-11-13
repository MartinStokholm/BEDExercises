using ModelManagementAPI.Controllers;
using ModelManagementAPI.Entities;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ModelManagementAPI.Hubs
{
    [HubName("expensehub")]
    public class ExpenseHub : Hub
    {
        public ExpenseHub()
        {

        }
    }
}
