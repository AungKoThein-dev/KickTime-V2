using KickTime.Core.Entities;

namespace KickTime.Application.Authentication.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, string roleName);
    }
}
