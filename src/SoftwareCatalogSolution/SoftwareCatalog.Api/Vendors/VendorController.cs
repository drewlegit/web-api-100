using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors;

public class VendorController(IDocumentSession session) : ControllerBase
{

    [HttpPost("/vendors")]
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] VendorCreateModel request
        )
    {

        var entity = new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedAt = DateTimeOffset.UtcNow,
            Link = request.Link
        };

        session.Store(entity);
        await session.SaveChangesAsync();

        var response = new VendorDetailsResponseModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            Link = entity.Link,
            CreatedAt = entity.CreatedAt
        };
        return StatusCode(201, response);
    }


    [HttpGet("/vendors/{id:guid}")]
    public async Task<ActionResult> GetVendorAsync(Guid id)
    {
        var entity = await session.Query<VendorEntity>().Where(v => v.Id == id).FirstOrDefaultAsync();

        var response = new VendorDetailsResponseModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            Link = entity.Link,
            CreatedAt = entity.CreatedAt

        };
        return Ok(response);

    }

}