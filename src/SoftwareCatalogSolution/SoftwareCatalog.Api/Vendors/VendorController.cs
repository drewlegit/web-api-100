using Marten;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace SoftwareCatalog.Api.Vendors;

public class VendorController(IDocumentSession session) : ControllerBase
{
    [HttpPost("/vendors")]
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] VendorCreateModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 2 || request.Name.Length > 100)
        {
            return BadRequest("Vendor name must be between 2 and 100 characters.");
        }

        if (!string.IsNullOrEmpty(request.Link) && !Regex.IsMatch(request.Link, @"^https://", RegexOptions.IgnoreCase))
        {
            return BadRequest("The website URL must start with https://");
        }

        var entity = new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedAt = DateTimeOffset.UtcNow,
            Link = request.Link?.ToLowerInvariant()
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

        if (entity == null)
        {
            return NotFound();
        }

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