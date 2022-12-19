using System;
using Microsoft.AspNetCore.Mvc;
using Assignment_4.Models;
using Assignment_4.Services;

namespace Assignment_4.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassesController : ControllerBase
{
    private readonly ClassesService _classesService;
    private readonly ILogger<ClassesController> _logger;
    public ClassesController(ClassesService classesService, ILogger<ClassesController> logger)
    {
        _classesService = classesService;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Class>>> GetClassItems()
    {
        _logger.LogInformation("Getting all classes");
        var classes = await _classesService.GetAsync();
        if (classes == null)
        {
            return NotFound();
        }

        return Ok(classes);

    }

    [HttpPost]
    public ActionResult Post()
    {
        _classesService.CreateClasses();
        return Ok();
    }
}


