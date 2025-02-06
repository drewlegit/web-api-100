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
            Link = "https://jetbrains.com"
        };
        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestEntity).ToUrl("/vendors");
            api.StatusCodeShouldBe(201);
        });

        var postBody = postResponse.ReadAsJson<VendorDetailsResponseModel>();

        Assert.NotNull(postBody);
        Assert.Equal("Jetbrains", postBody.Name);
        Assert.Equal("https://jetbrains.com", postBody.Link);
    }

    [Fact]
    public async Task CannotAddVendorWithShortName()
    {
        var host = await AlbaHost.For<Program>();
        var requestEntity = new VendorCreateModel()
        {
            Name = "A",
            Link = "https://jetbrains.com"
        };
        await host.Scenario(api =>
        {
            api.Post.Json(requestEntity).ToUrl("/vendors");
            api.StatusCodeShouldBe(400);
        });
    }

    [Fact]
    public async Task CannotAddVendorWithLongName()
    {
        var host = await AlbaHost.For<Program>();
        var requestEntity = new VendorCreateModel()
        {
            Name = new string('A', 101),
            Link = "https://jetbrains.com"
        };
        await host.Scenario(api =>
        {
            api.Post.Json(requestEntity).ToUrl("/vendors");
            api.StatusCodeShouldBe(400);
        });
    }

    [Fact]
    public async Task CannotAddVendorWithInvalidUrl()
    {
        var host = await AlbaHost.For<Program>();
        var requestEntity = new VendorCreateModel()
        {
            Name = "Valid Name",
            Link = "http://drewle.com"
        };
        await host.Scenario(api =>
        {
            api.Post.Json(requestEntity).ToUrl("/vendors");
            api.StatusCodeShouldBe(400);
        });
    }

    [Fact]
    public async Task CannotAddVendorWithoutName()
    {
        var host = await AlbaHost.For<Program>();
        var requestEntity = new VendorCreateModel()
        {
            Name = "",
            Link = "https://jetbrains.com"
        };
        await host.Scenario(api =>
        {
            api.Post.Json(requestEntity).ToUrl("/vendors");
            api.StatusCodeShouldBe(400);
        });
    }

    [Fact]
    public async Task CanAddVendorWithoutUrl()
    {
        var host = await AlbaHost.For<Program>();
        var requestEntity = new VendorCreateModel()
        {
            Name = "Vendor Without URL"
        };
        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestEntity).ToUrl("/vendors");
            api.StatusCodeShouldBe(201);
        });

        var postBody = postResponse.ReadAsJson<VendorDetailsResponseModel>();

        Assert.NotNull(postBody);
        Assert.Equal("Vendor Without URL", postBody.Name);
        Assert.Null(postBody.Link);
    }
}