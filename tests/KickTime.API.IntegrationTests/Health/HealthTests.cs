using KickTime.API.IntegrationTests.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;

namespace KickTime.API.IntegrationTests.Health;

public sealed class HealthTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public HealthTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Api_Should_Start_Successfully()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/health");

        Assert.True(response.IsSuccessStatusCode);
    }
}