using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using RestAPI.DTO;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly DataContext _context;

        public ExpensesController(DataContext context)
        {
            _context = context;
        }

        // POST: api/expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(ExpenseDTO expenseDTO)
        {
            Expense tempExpense = new Expense();
            tempExpense.Adapt(expenseDTO);
            _context.Expenses.Add(tempExpense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expenseDTO.Id }, expenseDTO);
        }

        [HttpGet]
        private async Task<ActionResult<Expense>> GetExpense(long id) 
        {
            var expenseItem = await _context.Expenses.FindAsync(id);
            
            if (expenseItem == null)
            {
                return NotFound();
            }
            
            return expenseItem;
        }

    }
}
