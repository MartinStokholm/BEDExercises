using Microsoft.AspNetCore.Mvc;
using Assignment_4.Models;
using Assignment_4.Services;


namespace Assignment_4.Controllers;

[ApiController]
[Route("[controller]")]

public class TypesController : ControllerBase
{
    private TypesService _typesService;
    private readonly ILogger<TypesController> _logger;
    public TypesController(TypesService cardTypeService, ILogger<TypesController> logger)
    {
        _typesService = cardTypeService;
        _logger = logger;
    }
   
    [HttpGet]
    public async Task<ActionResult<Types>> GetCardType()
    {
        _logger.LogInformation("Getting all types");
        var cardtype = await _typesService.GetAsync();

        if (cardtype == null)
        {
            return NotFound();
        }

        return Ok(cardtype);

    }

    [HttpPost]
    public ActionResult Post()
    {
        _typesService.CreateCardTypes();
        return Ok();
    }

}
