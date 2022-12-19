using System;
using Microsoft.AspNetCore.Mvc;
using Assignment_4.Models;
using Assignment_4.Services;


namespace Assignment_4.Controllers;
[ApiController]
[Route("[controller]")]
public class SetsController : ControllerBase
{
    private readonly SetsService _setsService;
    private readonly ILogger<SetsController> _logger;

    public SetsController(SetsService setsService, ILogger<SetsController> logger)
    {
        _setsService = setsService;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Set>>> GetSet()
    {
        _logger.LogInformation("Getting all sets");
        var set = await _setsService.GetAsync();
        if (set == null)
        {
            return NotFound();
        }
        return Ok(set);

    }

    [HttpPost]
    public ActionResult Post()
    {
        _setsService.CreateSets();
        return Ok();
    }
}


