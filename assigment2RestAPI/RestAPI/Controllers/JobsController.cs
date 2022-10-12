using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.DTO;
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

        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(JobDTO jobDTO)
        {
            Job tempJob = new Job();
            jobDTO.Adapt(tempJob);
            _context.Jobs.Add(tempJob);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = tempJob.Id }, jobDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PostModelJob(long id, long modelId)
        {
            var job = await _context.Jobs.FindAsync(id);
            var model = await _context.Models.FindAsync(id);
            if (job == null || model == null || job.Models == null)
                return NotFound();

            job.Models.Add(model);

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


        [HttpPatch("{id}")]
        public async Task<IActionResult> PutJob(long id, JobUpdateDTO job)
        {

            var tempJob = await _context.Jobs.FindAsync(id);
            job.Adapt(tempJob);
            if (tempJob == null)
                return NotFound();

            _context.Entry(tempJob).State = EntityState.Modified;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobModelDTO>>> GetJob()
        {

            var tempJobs = new List<JobModelDTO>();

            var jobs = await _context.Jobs.Include(x => x.Models).ToListAsync();
            foreach (var job in jobs)
            {
                var tempJob = new JobModelDTO();
                job.Adapt(tempJob);
                tempJobs.Add((tempJob));
            }

            return tempJobs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(long id)
        {
            var job = await _context.Jobs
                .Include(x => x.Models)
                .Include(x => x.Expenses)
                .FirstOrDefaultAsync(x => x.Id == id);

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
