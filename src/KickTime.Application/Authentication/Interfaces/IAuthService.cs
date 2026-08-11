using KickTime.Core.Common;
using KickTime.Core.DTOs.Auth;

namespace KickTime.Application.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<RegisterResponse>> RegisterAsync(
            RegisterRequest request,
            CancellationToken cancellationToken);

        Task<ApiResponse<LoginResponse>> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken);

        Task<ApiResponse<UserProfileResponse>> GetProfileAsync(
            long userId,
            CancellationToken cancellationToken);
    }
}
