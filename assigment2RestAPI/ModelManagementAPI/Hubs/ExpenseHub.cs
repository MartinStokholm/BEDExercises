using ModelManagementAPI.Controllers;
using ModelManagementAPI.Entities;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ModelManagementAPI.Hubs
{
    [HubName("expenseHub")]
    public class ExpenseHub : Hub
    {
        public ExpenseHub()
        {

        }
    }
}
