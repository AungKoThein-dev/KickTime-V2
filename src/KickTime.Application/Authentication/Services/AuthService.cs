using KickTime.Application.Authentication.Interfaces;
using KickTime.Application.Authentication.Mappings;
using KickTime.Core.Common;
using KickTime.Core.Constants;
using KickTime.Core.DTOs.Auth;
using KickTime.Core.Entities;
using Microsoft.Extensions.Logging;

namespace KickTime.Application.Authentication.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        private readonly IRoleRepository _roleRepository;

        private readonly IPasswordHasher _passwordHasher;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            ILogger<AuthService> logger)
        {
            ArgumentNullException.ThrowIfNull(userRepository);
            ArgumentNullException.ThrowIfNull(roleRepository);
            ArgumentNullException.ThrowIfNull(passwordHasher);
            ArgumentNullException.ThrowIfNull(jwtTokenGenerator);
            ArgumentNullException.ThrowIfNull(logger);
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        public async Task<ApiResponse<RegisterResponse>> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsByEmailAsync(
                request.Email,
                cancellationToken))
            {
                return new ApiResponse<RegisterResponse>
                {
                    Success = false,
                    Message = Messages.EmailAlreadyExists
                };
            }

            var role = await _roleRepository.GetByNameAsync(
                SystemRoles.User,
                cancellationToken);

            if (role is null)
            {
                throw new InvalidOperationException(
                    "User role is not configured.");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = _passwordHasher.Hash(request.Password),
                RoleId = role.Id,
                IsActive = true
            };

            user.Id = await _userRepository.CreateAsync(
                user,
                cancellationToken);

            _logger.LogInformation(
                "User {Email} registered successfully.",
                user.Email);

            return new ApiResponse<RegisterResponse>
            {
                Success = true,
                Message = Messages.RegistrationSuccess,
                Data = new RegisterResponse
                {
                    UserId = user.Id
                }
            };
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(
                request.Email,
                cancellationToken);

            if (user is null || !_passwordHasher.Verify(
                request.Password,
                user.PasswordHash))
            {
                _logger.LogWarning(
                    "Failed login attempt for {Email}",
                    request.Email);

                return new ApiResponse<LoginResponse>
                {
                    Success = false,
                    Message = Messages.InvalidCredential
                };
            }
            var role = await _roleRepository.GetByIdAsync(
                user.RoleId,
                cancellationToken);

            if (role is null)
            {
                _logger.LogError(
                "Role {RoleId} was not found.",
                user.RoleId);

                return new ApiResponse<LoginResponse>
                {
                    Success = false,
                    Message = Messages.RoleNotFound
                };
            }

            var token = _jwtTokenGenerator.GenerateToken(
                    user,
                    role.Name);

            _logger.LogInformation(
                "User {Email} logged in successfully.",
                user.Email);

            return new ApiResponse<LoginResponse>
            {
                Success = true,
                Message = Messages.LoginSuccess,
                Data = new LoginResponse
                {
                    //Token = token
                }
            };
        }

        public async Task<ApiResponse<UserProfileResponse>> GetProfileAsync(
            long userId,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                userId,
                cancellationToken);

            if (user is null)
            {
                return new ApiResponse<UserProfileResponse>
                {
                    Success = false,
                    Message = Messages.UserNotFound
                };
            }
            var role = await _roleRepository.GetByIdAsync(
                user.RoleId,
                cancellationToken);

            if (role is null)
            {
                _logger.LogError(
                    "Role {RoleId} was not found.",
                    user.RoleId);

                return new ApiResponse<UserProfileResponse>
                {
                    Success = false,
                    Message = Messages.RoleNotFound
                };
            }

            return new ApiResponse<UserProfileResponse>
            {
                Success = true,
                Message = Messages.ProfileRetrieved,
                Data = user.ToProfileResponse(role.Name)
            };
        }
    }
}
