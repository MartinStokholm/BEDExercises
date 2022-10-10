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
        public async Task<ActionResult<Expense>> PostExpense(Expense request)
        {
            _context.Expenses.Add(request);
            await _context.SaveChangesAsync();

            return Ok(await _context.Expenses.ToListAsync());
        }        

    }
}
