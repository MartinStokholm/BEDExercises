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
    public class JobsController : ControllerBase
    {
        private readonly DataContext _context;

        public JobsController(DataContext context)
        {
            _context = context;
        }

        // POST a Job
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job request)
        {
            _context.Jobs.Add(request);
            await _context.SaveChangesAsync();

            return Ok(await _context.Jobs.ToListAsync());
        }

        // DELETE a Job
        [HttpDelete("{id}")]
        public async Task<ActionResult<Job>> DeleteJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return Ok(await _context.Jobs.ToListAsync());
        }

        // PATCH JobUpdate
        [HttpPatch("{id}")]
        public async Task<ActionResult<JobUpdate>> PatchJob(long id, JobUpdate updatedJob)
        {
            if (id != updatedJob.Id)
            {
                return BadRequest();
            }
            var dbJob = await _context.Jobs.FindAsync(id);
            if (dbJob == null)
            {
                return NotFound();
            }

            dbJob.Adapt(updatedJob);

            _context.Entry(dbJob).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(await _context.Jobs.ToListAsync());
        }

        // PUT {model.id} on Job
        [HttpPut("{modelId} {jobId}")]
        public async Task<ActionResult<Job>> PutModelOnJob(long modelId, long jobId)
        {
            var dbJob = await _context.Jobs.FindAsync(jobId);
            
            if (dbJob == null)
            {
                return NotFound("Could not find job with id " + jobId);
            }
            
            var dbModel = await _context.Models.FindAsync(modelId);
            
            if (dbModel == null)
            {
                return NotFound("Could not find model with id " + modelId);
            }

            
            dbJob.Models.Add(dbModel);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.Jobs.ToListAsync());

        }

        // DELETE {model.id} from Job
        [HttpDelete("{modelId} {jobId}")]
        public async Task<ActionResult<Job>> DeleteModelFromJob(long modelId, long jobId)
        {
            var dbJob = await _context.Jobs.FindAsync(jobId);

            if (dbJob == null)
            {
                return NotFound("Could not find job with id " + jobId);
            }

            var dbModel = await _context.Models.FindAsync(modelId);

            if (dbModel == null)
            {
                return NotFound("Could not find model with id " + modelId);
            }


            dbJob.Models.Remove(dbModel);
            await _context.SaveChangesAsync();

            return Ok(await _context.Jobs.ToListAsync());

        }

        // GET JobWithModels
        [HttpGet]
        public async Task<ActionResult<JobWithModels>> GetJobWithModels()
        {
            var jobs = await _context.Jobs.ToListAsync();
            
            var jobWithModels = jobs.Adapt<JobWithModels>();
            
            return Ok(jobWithModels);
        }

        // GET {model.id} JobWithModels
        // get all jobs related to a model by id
        [HttpGet("{modelId}")]
        public async Task<ActionResult<JobWithModels>> GetJobWithModels(long modelId)
        {
            var dbModel = await _context.Models.FindAsync(modelId);

            if (dbModel == null)
            {
                return NotFound("Could not find model with id " + modelId);
            }

            var jobs = await _context.Jobs.Where(j => j.Models.Contains(dbModel)).ToListAsync();

            var jobWithModels = jobs.Adapt<JobWithModels>();

            return Ok(jobWithModels);
        }

        // GET {job.id} JobWithExpenses
        // get job by id with all expenses related to that job
        [HttpGet("{jobId}")]
        public async Task<ActionResult<JobWithExpenses>> GetJobWithExpenses(long jobId)
        {
            var dbJob = await _context.Jobs.FindAsync(jobId);

            if (dbJob == null)
            {
                return NotFound("Could not find job with id " + jobId);
            }
            
            var jobWithExpenses = dbJob.Adapt<JobWithExpenses>();

            return Ok(jobWithExpenses);
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
