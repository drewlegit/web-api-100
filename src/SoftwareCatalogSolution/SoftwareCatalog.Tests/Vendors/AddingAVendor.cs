using Alba;
using SoftwareCatalog.Api.Vendors;

namespace SoftwareCatalog.Tests.Vendors;

public class AddingAVendor
{
    [Fact]
    public async Task CanAddAVendor()
    {
        var host = await AlbaHost.For<Program>();
        var requestEntity = new VendorCreateModel()
        {
            Name = "Jetbrains",
            Link = "Https://jetbrains.com"
        };
        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestEntity).ToUrl("/vendors");
            api.StatusCodeShouldBe(201);
        });

        var postBody = postResponse.ReadAsJson<VendorDetailsResponseModel>();

        Assert.NotNull(postBody);

        Assert.Equal("Jetbrains", postBody.Name);
        Assert.Equal("Https://jetbrains.com", postBody.Link);

        await host.Scenario(api =>
        {
            api.Post.Json(requestEntity).ToUrl("/vendors");
            api.StatusCodeShouldBe(201);
        });
    }
}
