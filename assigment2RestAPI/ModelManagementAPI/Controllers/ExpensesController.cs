using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR;
using ModelManagementAPI.Hubs;
using ModelManagementAPI.Entities;


namespace ModelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly Microsoft.AspNetCore.SignalR.IHubContext<MessageHub> _hubContext;

        public ExpensesController(DataContext context, Microsoft.AspNetCore.SignalR.IHubContext<MessageHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        

        // POST Expense
        [HttpPost]
        public async Task<ActionResult<List<ExpenseCreate>>> PostExpense(ExpenseCreate expenseCreate)
        {
            // add the expense to the database and save changes
            _context.Expenses.Add(expenseCreate.Adapt<Expense>());
            await _context.SaveChangesAsync();

            // Find model names from model in db and add to list
            var dbModelNames = from m in _context.Models
                          select m.FirstName;
            List<string> modelNames = dbModelNames.ToList();

            // Find customer names from job in db and add to list
            var dbcustomerName = from j in _context.Jobs
                                 where j.Id == expenseCreate.JobId
                                 select j.Customer;
            List<string> customerNames = dbcustomerName.ToList();

            await _hubContext.Clients.All.SendAsync("ReceiveMessage",expenseCreate, modelNames, customerNames);

            return Accepted();

        }        
    }
}
