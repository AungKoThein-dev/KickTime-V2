using KickTime.API.Controller;
using KickTime.Application.Authentication.Interfaces;
using KickTime.Application.Results;
using KickTime.Core.Constants;
using KickTime.Core.DTOs.Auth;
using KickTime.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KickTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request, cancellationToken);

            return HandleResult(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request, cancellationToken);

            return HandleResult(result);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim))
            {
                return Unauthorized(new { Message = "User id claim is missing." });
            }

            if (!long.TryParse(userIdClaim, out var userId))
            {
                return BadRequest(new { Message = "Invalid user id format. Expected integer." });
            }

            var result = await _authService.GetProfileAsync(userId, cancellationToken);

            return HandleResult(result);
        }
    }
}
