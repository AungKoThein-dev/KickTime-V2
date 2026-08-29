using KickTime.API.Controller;
using KickTime.Application.Authentication.Interfaces;
using KickTime.Application.Common.Interfaces;
using KickTime.Core.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KickTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        private readonly ICurrentUserService _currentUserService;


        public AuthController(IAuthService authService, ICurrentUserService currentUserService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request, cancellationToken);

            return HandleResult(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request, cancellationToken);

            return HandleResult(result);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId is null)
            {
                return Unauthorized("User id claim is missing.");
            }

            var result = await _authService.GetProfileAsync((long)userId, cancellationToken);

            return HandleResult(result);
        }
    }
}
