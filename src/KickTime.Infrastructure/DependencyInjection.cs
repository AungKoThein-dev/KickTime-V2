using KickTime.Application.Authentication.Interfaces;
using KickTime.Application.Booking.Interfaces;
using KickTime.Application.Court.Interfaces;
using KickTime.Application.Stadium.Interfaces;
using KickTime.Infrastructure.Authentication.Repositories;
using KickTime.Infrastructure.Authentication.Security;
using KickTime.Infrastructure.Booking.Repositories;
using KickTime.Infrastructure.Configuration;
using KickTime.Infrastructure.Court.Repositories;
using KickTime.Infrastructure.Database;
using KickTime.Infrastructure.Stadium.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KickTime.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.AddSingleton<IDbConnectionFactory, SqlServerConnectionFactory>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped<IStadiumRepository, StadiumRepository>();

        services.AddScoped<ICourtRepository, CourtRepository>();

        services.AddScoped<IBookingRepository, BookingRepository>();

        return services;
    }
}