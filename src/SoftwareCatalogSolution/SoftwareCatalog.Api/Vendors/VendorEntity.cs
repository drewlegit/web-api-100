
namespace SoftwareCatalog.Api.Vendors;

public class VendorEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Link { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}