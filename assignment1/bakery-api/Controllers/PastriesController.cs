using Microsoft.AspNetCore.Mvc;
using bakery_data.Models;
using bakery_api.Data;

namespace bakery_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PastriesController : ControllerBase {
  [HttpGet(Name = "GetPastries")]
  public IList<Pastry> Get() {
    return Pastries.PastriesList;
  }
}