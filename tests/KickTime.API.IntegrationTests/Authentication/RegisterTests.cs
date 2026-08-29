using KickTime.API.Contracts;
using KickTime.API.IntegrationTests.Infrastructure;
using KickTime.Core.DTOs.Auth;
using System.Net;
using System.Net.Http.Json;

namespace KickTime.API.IntegrationTests.Authentication;

public sealed class RegisterTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public RegisterTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_WithValidRequest_ShouldCreateUserAndReturnSuccess()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Name = "Integration Test User",
            Email = $"test_{Guid.NewGuid():N}@kicktime.test",
            Phone = "09123456789",
            Password = "TestPassword123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/Auth/register",
            request);

        // Assert - HTTP
        Assert.True(
            response.IsSuccessStatusCode,
            await response.Content.ReadAsStringAsync());

        // Assert - API contract
        var content = await response.Content
            .ReadFromJsonAsync<ApiResponse<RegisterResponse>>();

        Assert.NotNull(content);
        Assert.True(content.Success);
        Assert.NotNull(content.Data);
        Assert.True(content.Data.UserId > 0);
    }

    [Fact]
    public async Task Register_WithDuplicateEmail_ShouldReturnConflict()
    {
        // Arrange
        var email = $"duplicate_{Guid.NewGuid():N}@kicktime.test";

        var firstRequest = new RegisterRequest
        {
            Name = "First Test User",
            Email = email,
            Phone = "09123456700",
            Password = "TestPassword123!"
        };

        var secondRequest = new RegisterRequest
        {
            Name = "Second Test User",
            Email = email,
            Phone = "09987654300",
            Password = "TestPassword123!"
        };

        // Act - First registration
        var firstResponse = await _client.PostAsJsonAsync(
            "/api/Auth/register",
            firstRequest);

        // Assert - First registration succeeds
        Assert.True(
            firstResponse.IsSuccessStatusCode,
            await firstResponse.Content.ReadAsStringAsync());

        // Act - Register again with the same email
        var secondResponse = await _client.PostAsJsonAsync(
            "/api/Auth/register",
            secondRequest);

        // Assert

        Assert.Equal(
            HttpStatusCode.Conflict,
            secondResponse.StatusCode);

        var content = await secondResponse.Content.ReadFromJsonAsync<ApiResponse<RegisterResponse>>();
        Assert.NotNull(content);
        Assert.False(content.Success);
        Assert.False(string.IsNullOrWhiteSpace(content.Message));
    }


}