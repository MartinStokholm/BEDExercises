using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly DataContext _context;

        public JobsController(DataContext context)
        {
            _context = context;
        }
        
        // POST: api/Jobs
        /// <summary>
        /// Create new job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }
        
        // DELETE: api/Jobs/5
        /// <summary>
        /// Delete a job
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // PATCH: api/Jobs/5
        /// <summary>
        /// Update StartDate, Days, Location and Comments for a job
        /// </summary>
        /// <param name="id"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutJob(long id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        /// <summary>
        /// Add a model by ID to a job by ID
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="jobID"></param>
        /// <returns></returns>
        //public async Task<IActionResult> AddModelToJob(long modelID, long jobID)
        //{
        //    var model = await _context.Model.FindAsync(modelID);
        //    var job = await _context.Job.FindAsync(jobID);
        //    if (model == null || job == null)
        //    {
        //        return NotFound();
        //    }
        //    // job.Model = model;
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
      

        /// <summary>
        /// Delete a model from a job
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{model}")]
        public async Task<IActionResult> DeleteModel(long id)
        {
            {
                var model = await _context.Models.FindAsync(id);
                if (model == null)
                {
                    return NotFound();
                }

                _context.Models.Remove(model);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

        // GET: api/Jobs
        /// <summary>
        /// Get list of all jobs
        /// Includes name of model that is on each job but no expenses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJob()
        {
            return await _context.Jobs.ToListAsync();
        }

        //public async Task<ActionResult<IEnumerable<Job>>> GetJobWithModel()
        //{
        //    return await _context.Job.Include(j => j.Models).ToListAsync();
        //}

        // GET: api/Jobs/2
        /// <summary>
        /// Get job by JobID that contains a list of all expenses from that job
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
