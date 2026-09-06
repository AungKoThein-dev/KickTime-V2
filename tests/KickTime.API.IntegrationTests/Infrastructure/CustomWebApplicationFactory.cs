using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace KickTime.API.IntegrationTests.Infrastructure;

public sealed class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(
        IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration(
            (_, configuration) =>
            {
                var testConfigurationPath = Path.Combine(
                    AppContext.BaseDirectory,
                    "appsettings.Testing.json");

                configuration.AddJsonFile(
                    testConfigurationPath,
                    optional: false,
                    reloadOnChange: false);

                configuration.AddEnvironmentVariables();
            });
    }
}