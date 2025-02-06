namespace SoftwareCatalog.Api.Vendors;

public record VendorCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string? Link { get; set; } = null;
}

public record VendorDetailsResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Link { get; set; } = null;
    public DateTimeOffset CreatedAt { get; set; }
}
