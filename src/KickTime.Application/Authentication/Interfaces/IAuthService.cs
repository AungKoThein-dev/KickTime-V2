using KickTime.Application.Results;
using KickTime.Core.DTOs.Auth;

namespace KickTime.Application.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task<Result<RegisterResponse>> RegisterAsync(
            RegisterRequest request,
            CancellationToken cancellationToken);

        Task<Result<LoginResponse>> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken);

        Task<Result<UserProfileResponse>> GetProfileAsync(
            long userId,
            CancellationToken cancellationToken);
    }
}
