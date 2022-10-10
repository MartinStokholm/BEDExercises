using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.DTO;
using RestAPI.Models;

namespace RestAPI.Controllers
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
        
        // POST: api/Models
        /// <summary>
        /// Creates a new Model only base data no jobs or expenses
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Model>> PostModel(ModelDTO modelDTO)
        {
            var tempModel = new Model();
            tempModel.Adapt(modelDTO);
            _context.Models.Add(tempModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModel", new { id = modelDTO.Id }, modelDTO);
        }
        
        // DELETE: api/Models/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(long id)
        {
            var modelItem = await _context.Models.FindAsync(id);
            if (modelItem == null)
            {
                return NotFound();
            }

            _context.Models.Remove(modelItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // PATCH: api/Models/5
        /// <summary>
        /// Update a model with only base data no jobs or expenses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutModel(long id, Model modelItem)
        {
            if (id != modelItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(modelItem).State = EntityState.Modified;

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

            return NoContent();
        }
        
        // GET: api/Models
        /// <summary>
        /// Get all models as a list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model>>> GetModel()
        {
            return await _context.Models.ToListAsync();
        }

        // GET: api/Models/5
        /// <summary>
        /// Get a specific model based on modelID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ModelID and the jobs and expenses related to that model</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Model>> GetModel(long id)
        {
            var modelItem = await _context.Models.FindAsync(id);

            if (modelItem == null)
            {
                return NotFound();
            }

            return modelItem;
        }

        private bool ModelExists(long id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
    }
}
