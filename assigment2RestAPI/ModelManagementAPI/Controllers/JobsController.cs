using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelManagementAPI.Entities;
using System.Reflection.Metadata;


namespace ModelManagementAPI.Controllers
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
        public async Task<ActionResult<Job>> PutJob(long jobId, JobUpdate jobUpdate)
        {
            // get the model from the database
            var dbJob = await _context.Jobs.FindAsync(jobId);
            if (dbJob == null) { return NotFound("Could not find Job"); }

            // update the model in the database using mapster adapt
            var job = jobUpdate.Adapt(dbJob);
            _context.Jobs.Update(job);
            
            // save the changes
            await _context.SaveChangesAsync();

            // return the updated job using mapster adapt
            return Ok(dbJob);
        }

        // PUT {model.id} on Job
        [HttpPut("{jobId}/AddModel/{modelId}")]
        public async Task<ActionResult<JobWithModels>> PutModelOnJob(long modelId, long jobId)
        {
            var dbModel = await _context.Models.SingleAsync(m => m.Id == modelId);
            if (dbModel == null) { return NotFound("Could not find Model"); }

            var dbJob = await _context.Jobs.SingleAsync(j => j.Id == jobId);
            if (dbJob == null) { return NotFound("Could not find Job"); }
            _context.Entry(dbJob)
                .Collection(j => j.Models)
                .Load();
            
            if (dbJob.Models.Contains(dbModel)) { return Conflict("Model already on Job"); }

            _context.Entry(dbModel)
                .Collection(m => m.Jobs)
                .Load();

            dbJob.Models.Add(dbModel);
            await _context.SaveChangesAsync();
            
            return Accepted(dbModel);
        }

        // DELETE {model.id} from Job
        [HttpDelete("{jobId}/RemoveModel/{modelId}")]
        public async Task<ActionResult<Job>> DeleteModelFromJob(long modelId, long jobId)
        {
            var dbJob = await _context.Jobs.FindAsync(jobId);
            
            if (dbJob == null) { return NotFound("Could not find job with id " + jobId); }
            
            _context.Entry(dbJob)
                .Collection(j => j.Models)
                .Load();

            var dbModel = await _context.Models.FindAsync(modelId);
            if (dbModel == null) { return NotFound("Could not find model with id " + modelId); }

            if (!dbJob.Models.Contains(dbModel)) { return BadRequest("Model not on job"); }

            dbJob.Models.Remove(dbModel);
            await _context.SaveChangesAsync();

            return Accepted(dbModel);

        }

        // GET JobWithModels
        [HttpGet("WithModelNames/{modelId}")]
        [HttpGet]
        public async Task<ActionResult<JobWithModelNames>> GetJobWithModelNames()
        {
            List<JobWithModelNames> allJobsWithModelNames = new List<JobWithModelNames>();

            // loop throough all jobs in db and load all jobs with models
            foreach (var job in _context.Jobs)
            {
                // explicit loading of models for each job
                await _context.Entry(job).Collection(j => j.Models).LoadAsync();

                var aJobWithModelNames = job.Adapt<JobWithModelNames>();
                aJobWithModelNames.ModelNames = new List<string>();
                foreach (var model in job.Models)
                {
                    aJobWithModelNames.ModelNames.Add($"{model.FirstName} {model.LastName}");
                }
                allJobsWithModelNames.Add(aJobWithModelNames);
            }

            return Ok(allJobsWithModelNames);
        }

        // GET {model.id} ModelOnJob
        // get all jobs related to a model by id
        [HttpGet("WithModel/{modelId}")]
        public async Task<ActionResult<List<Job>>> GetJobsWithModels(long modelId)
        {
            // get all jobs from db
            var dbJobs = await _context.Jobs.ToListAsync();

            dbJobs.ForEach(async j => await _context.Entry(j)
                .Collection(j => j.Models)
                .LoadAsync());
            
            // find jobs where model id matches model id for a job
            dbJobs = dbJobs
                .Where(j => j.Models.Any(m => m.Id == modelId))
                .ToList();  
            
            return Ok(dbJobs);
        }

        // GET {job.id} JobWithExpenses
        [HttpGet("/WithExpenses/{jobId}")]
        public async Task<ActionResult<JobWithExpenses>> GetJobWithExpenses(long jobId)
        {
            var dbJob = await _context.Jobs.FindAsync(jobId);

            if (dbJob == null) { return NotFound("Could not find job with id " + jobId); }

            _context.Entry(dbJob)
                .Collection(j => j.Expenses)
                .Load();
            _context.Entry(dbJob)
                .Collection(j => j.Models)
                .Load();

            // map the job to a jobWithExpenses and assign a new list of expenses to the jobWithExpenses list of expenses
            var jobWithExpenses = dbJob.Adapt<JobWithExpenses>();
            jobWithExpenses.Expenses = new List<ExpenseCreate>();

            if (dbJob == null) { return NotFound("Could not find job with id " + jobId); }

            // add all expenses related to the job to the jobWithExpenses object
            foreach (var expense in dbJob.Expenses)
            {
                jobWithExpenses.Expenses.Add(expense.Adapt<ExpenseCreate>());
            }
            
            return Ok(jobWithExpenses);
        }

    }
}
