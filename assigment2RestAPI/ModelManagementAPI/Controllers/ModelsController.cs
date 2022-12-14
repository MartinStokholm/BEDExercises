using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelManagementAPI.Entities;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace ModelManagementAPI.Controllers
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
        public async Task<ActionResult<ModelBaseData>> PostModel(ModelBaseData modelCreate)
        {
            // use Mapster to map modelCreate to a Model
            var model = modelCreate.Adapt<Model>();

            // add the model to the database and save changes
            _context.Models.Add(model);
            await _context.SaveChangesAsync();

            // return the model as a ModelBaseData
            return Ok(model.Adapt<ModelBaseData>());

        }

        // DELETE Model
        [HttpDelete("{modelId}")]
        public async Task<ActionResult<Model>> DeleteModel(long modelId)
        {
            // get the model from the database
            var dbModel = await _context.Models.FindAsync(modelId);
            if (dbModel == null)
            {
                return NotFound("Model not found");
            }

            // delete the model from the database
            _context.Models.Remove(dbModel);

            
            _context.Entry(dbModel).State = EntityState.Deleted;

            // save changes
            await _context.SaveChangesAsync();

            // return the updated list of models
            return Ok(await _context.Models.ToListAsync());
        }

        // PUT ModelBasedata
        [HttpPut("BaseData/{modelId}")]
        public async Task<ActionResult<ModelBaseData>> PutModel(long modelId, ModelBaseData modelBaseData)
        {
            // get the model from the database
            var dbModel = await _context.Models.FindAsync(modelId);
            if (dbModel == null) { return NotFound("Could not find Model"); }
          
            // update the model in the database using mapster adapt
            var model = modelBaseData.Adapt(dbModel);
            _context.Models.Update(model);
            
            // save the changes
            await _context.SaveChangesAsync();

            // return the updated modelBaseData using mapster adapt
            return Ok(model.Adapt<ModelBaseData>());

        }

        // GET ModelBasedata
        [HttpGet("BaseData")]
        public async Task<ActionResult<List<ModelBaseData>>> GetModels()
        {
            var dbModel = await _context.Models.ToListAsync();
            if (dbModel == null) { return BadRequest("Could not find any models"); }

            foreach (var model in dbModel)
            {
                _context.Entry(model)
                    .Collection(m => m.Jobs)
                    .Load();
            }

            return Ok(dbModel.Adapt<List<ModelBaseData>>());
        }

        // GET {model.id} Model
        [HttpGet("{modelId}")]
        public async Task<ActionResult<Model>> GetModel(long modelId)
        {
            // get the model from the database
            var dbModel = await _context.Models.FindAsync(modelId);          

            if (dbModel == null) { return NotFound("Could not find Model"); }
            
            _context.Entry(dbModel)
             .Collection(m => m.Jobs)
             .Load();

            _context.Entry(dbModel)
             .Collection(m => m.Expenses)
             .Load();

            return Ok(dbModel);
        }
        
        private bool ModelExists(long id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
    }
}
