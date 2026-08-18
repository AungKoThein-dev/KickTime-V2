using KickTime.Application.Common.Interfaces;
using System.Security.Claims;

namespace KickTime.API.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor =
            httpContextAccessor
            ?? throw new ArgumentNullException(
                nameof(httpContextAccessor));
    }

    public long? UserId
    {
        get
        {
            var claim =
                _httpContextAccessor.HttpContext?
                    .User
                    .FindFirstValue(
                        ClaimTypes.NameIdentifier);

            return long.TryParse(claim, out var userId)
                ? userId
                : null;
        }
    }
}