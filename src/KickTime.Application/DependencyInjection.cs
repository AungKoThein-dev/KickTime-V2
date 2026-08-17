using FluentValidation;
using KickTime.Application.Authentication.Interfaces;
using KickTime.Application.Authentication.Services;
using KickTime.Application.Booking.Interfaces;
using KickTime.Application.Booking.Services;
using KickTime.Application.Court.Interfaces;
using KickTime.Application.Court.Services;
using KickTime.Application.Stadium.Interfaces;
using KickTime.Application.Stadium.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KickTime.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IStadiumService, StadiumService>();
        services.AddScoped<ICourtService, CourtService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly());
        return services;
    }
}