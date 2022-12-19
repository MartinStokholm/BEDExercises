using System;
using Microsoft.AspNetCore.Mvc;
using Assignment_4.Models;
using Assignment_4.Services;
using Assignment_4.Models.Dto;

namespace Assignment_4.Controllers;
[ApiController]
[Route("[controller]")]

public class CardsController : ControllerBase
{
    private readonly CardsService _cardsService;
    private readonly ILogger<CardsController> _logger;

    public CardsController(CardsService cardsService, ILogger<CardsController> logger)
    {
        _cardsService = cardsService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CardWithMetaDataDto>>> GetCardsAsync([FromQuery] QueryParams queryParams)
    {
        _logger.LogInformation(
            $"page={queryParams.page} " + 
            $"artist={queryParams.artist} " + 
            $"rarirtyId={queryParams.rarityid} " +
            $"classId={queryParams.classid} " +
            $"setId={queryParams.setid} ");
        var result = await _cardsService.GetCardsByQueryAsync(queryParams);

        _logger.LogInformation($"EntityCount={result.Count} ");
        return Ok(result);
    }

    [HttpPost]
    public ActionResult SeedData()
    {
        _cardsService.CreateCards();
        return Ok();
    }
}

