using Microsoft.AspNetCore.Mvc;
using bakery_data.Models;
using bakery_api.Data;

namespace bakery_api.Controllers;

[ApiController]
[Route("[controller]")]
public class BreadsController : ControllerBase {
  [HttpGet(Name = "GetBreads")]
  public IList<Bread> Get() {
    return Breads.BreadList;
  }
}