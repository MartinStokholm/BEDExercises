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
        public async Task<ActionResult<Job>> PostJob(JobCreate jobCreate)
        {
            // add the expense to the database and save changes
            _context.Jobs.Add(jobCreate.Adapt<Job>());
            await _context.SaveChangesAsync();

            // return updated list of jobs
            return Ok(await _context.Jobs.ToListAsync()); 
        }

        // DELETE a Job
        [HttpDelete("{jobId}")]
        public async Task<ActionResult<Job>> DeleteJob(long jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job == null)
            {
                return NotFound("Could not find job");
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return Ok(await _context.Jobs.ToListAsync());
        }

        // PUT JobUpdate
        [HttpPut("{jobId}")]
        public async Task<ActionResult<JobUpdate>> PutJob(long jobId, JobUpdate jobUpdate)
        {
            // get the model from the database
            var dbJob = await _context.Models.FindAsync(jobId);
            if (dbJob == null) { return NotFound("Could not find Model"); }

            // update the model in the database using mapster adapt
            var job = jobUpdate.Adapt(dbJob);
            _context.Models.Update(job);
            
            // save the changes
            await _context.SaveChangesAsync();

            // return the updated job using mapster adapt
            return Ok(job.Adapt<JobUpdate>());
        }

        // PUT {model.id} on Job
        [HttpPut("{jobId}/model/{modelId} ")]
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
            dbModel.Jobs.Add(dbJob);

            await _context.SaveChangesAsync();
            
            return Ok(await _context.Jobs.ToListAsync());

        }

        // DELETE {model.id} from Job
        [HttpDelete("{jobId}/model/{modelId} ")]
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
            return Ok(await _context.Jobs.Include(j => j.Models).ToListAsync());
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

    }
}
