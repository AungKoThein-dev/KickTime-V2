using KickTime.Application.Authentication.Interfaces;
using KickTime.Application.Authentication.Services;
using KickTime.Application.Stadium.Interfaces;
using KickTime.Application.Stadium.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KickTime.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IStadiumService, StadiumService>();
        return services;
    }
}