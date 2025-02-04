using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog;
/*
POST http://localhost:1337/vendors/{vendorId}/opensource
Content-Type: application/json

{
  "name": "Visual Studio Code"
}
*/
public class CatalogController : ControllerBase
{
    [HttpPost("/vendors/microsoft/opensource")]
    public async Task<ActionResult> AddItemToCatalogAsync(
        [FromBody] CatalogItemRequestModel request)
    {
        return Ok(request);
    }
}


public record CatalogItemRequestModel
{
    public string Name { get; set; } = string.Empty;
}