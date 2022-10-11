using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ModellingManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ModellingManagementAPI.Controllers
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

        // POST Expense
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expenseCreate)
        {
            // add the expense to the database and save changes
            _context.Expenses.Add(expenseCreate);
            await _context.SaveChangesAsync();

            // add expense to Model list of expenses
            var dbModel = await _context.Models.FindAsync(expenseCreate.ModelId);
            
            if (dbModel != null) 
            {
                // add the expense to the model update and save changes
                dbModel.Expenses.Add(expenseCreate);
                _context.Models.Update(dbModel);
                await _context.SaveChangesAsync();
                
                // return the expenses list 
                return Ok(await _context.Expenses.ToListAsync());
            }

            return BadRequest("Could not find model");
   
        }        
    }
}
