using System;
using Microsoft.AspNetCore.Mvc;
using Assignment_4.Models;
using Assignment_4.Services;


namespace Assignment_4.Controllers;
[ApiController]
[Route("[controller]")]

public class RaritiesController : ControllerBase
{
    RaritiesService _raritysService;
    private readonly ILogger<RaritiesController> _logger;
    public RaritiesController(RaritiesService raritiesService, ILogger<RaritiesController> logger)
    {
        _raritysService = raritiesService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Rarity>>> GetRarityItems()
    {
        _logger.LogInformation("Getting all rarities");
        var rarity = await _raritysService.GetAsync();
        if (rarity == null)
        {
            return NotFound();
        }

        return Ok(rarity);

    }
}


