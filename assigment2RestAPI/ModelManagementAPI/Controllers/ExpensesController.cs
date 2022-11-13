using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using ModelManagementAPI.Hubs;
using ModelManagementAPI.Entities;

namespace ModelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IHubContext<ExpenseHub> _hubContext;

        public ExpensesController(DataContext context, IHubContext<ExpenseHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        

        // POST Expense
        [HttpPost]
        public async Task<ActionResult<List<ExpenseCreated>>> PostExpense(ExpenseCreate expenseCreate)
        {
            var dbModel = _context.Models.Find(expenseCreate.ModelId);
            if (dbModel == null) { return NotFound("Model not found"); }
            
            _context.Entry(dbModel)
           .Collection(m => m.Expenses)
           .Load();
            
            // add the expense to the database and save changes
            _context.Expenses.Add(expenseCreate.Adapt<Expense>());
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("expenseadded", expenseCreate);

            var dbExpenses= await _context.Expenses.ToListAsync();

            return Accepted(dbExpenses.Adapt<List<ExpenseCreated>>());

        }        
    }
}
