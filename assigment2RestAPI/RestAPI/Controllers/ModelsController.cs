using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModellingManagementAPI.Models;


namespace ModellingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly DataContext _context;

        public ModelsController(DataContext context)
        {
            _context = context;
        }

        // POST ModelBasedata
        [HttpPost]
        public async Task<ActionResult<Model>> PostModel(Model newModel)
        {
            _context.Models.Add(newModel);
            await _context.SaveChangesAsync();

            return Ok(await _context.Models.ToListAsync());
        }

        // DELETE Model
        [HttpDelete("{id}")]
        public async Task<ActionResult<Model>> DeleteModel(long id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return Ok(await _context.Models.ToListAsync());
        }


        // PATCH ModelBasedata
        [HttpPatch("{id}")]
        public async Task<ActionResult<Model>> PatchModel(long id, ModelBaseData updatedModel)
        {
            if (id != updatedModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.Models.ToListAsync());
        }

        // GET ModelBasedata
        [HttpGet]
        public async Task<ActionResult<ModelBaseData>> GetModels()
        {
            return Ok(await _context.Models.ToListAsync());
        }

        // GET {model.id} Model
        [HttpGet("{id}")]
        public async Task<ActionResult<Model>> GetModel(long id)
        {
            var model = await _context.Models.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }
        
        private bool ModelExists(long id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
    }
}
