using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog.Endpoints;

public class AddingACatalogItem(IDocumentSession session) : ControllerBase
{
    // GET /catalog/pizza -> 404


    [HttpPost("/vendors/microsoft/opensource")]
    public async Task<ActionResult> AddOpenSourceItemToCatalogAsync(
        [FromBody] CatalogItemRequestModel request)
    {

        // TODO: 1. Do Some validation
        // TODO : 2 save it to the database.
        var entityToSave = new CatalogItemEntity
        {
            Id = Guid.NewGuid(),
            Licence = CatalogItemLicenceTypes.OpenSource,
            Name = request.Name,
            Vendor = "Microsoft"
        };

        session.Store(entityToSave);
        await session.SaveChangesAsync(); // Do the actual work and save into the database

        // Step 3: send them the response
        // fake this for right now
        var fakeResponse = new CatalogItemResponseDetailsModel
        {
            Id = entityToSave.Id,
            Licence = entityToSave.Licence,
            Name = entityToSave.Name,
            Vendor = entityToSave.Vendor
        };
        return StatusCode(201, fakeResponse);
    }
    [HttpPost("/vendors/microsoft/paid")]
    public async Task<ActionResult> AddPaidItemToCatalogAsync(
       [FromBody] CatalogItemRequestModel request)
    {

        // TODO: 1. Do Some validation
        // TODO : 2 save it to the database.
        var entityToSave = new CatalogItemEntity
        {
            Id = Guid.NewGuid(),
            Licence = CatalogItemLicenceTypes.Paid,
            Name = request.Name,
            Vendor = "Microsoft"
        };

        session.Store(entityToSave);
        await session.SaveChangesAsync(); // Do the actual work and save into the database

        // Step 3: send them the response
        // fake this for right now
        var fakeResponse = new CatalogItemResponseDetailsModel
        {
            Id = entityToSave.Id,
            Licence = entityToSave.Licence,
            Name = entityToSave.Name,
            Vendor = entityToSave.Vendor
        };
        return StatusCode(201, fakeResponse);
    }
}





