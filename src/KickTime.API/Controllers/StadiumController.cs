using KickTime.API.Controller;
using KickTime.Application.Stadium.Interfaces;
using KickTime.Core.Constants;
using KickTime.Core.DTOs.Stadium;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KickTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StadiumController : BaseController
{
    private readonly IStadiumService _stadiumService;

    public StadiumController(IStadiumService stadiumService)
    {
        _stadiumService = stadiumService
            ?? throw new ArgumentNullException(nameof(stadiumService));
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _stadiumService.GetAllAsync(
            cancellationToken);

        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await _stadiumService.GetByIdAsync(id, cancellationToken);

        return HandleResult(result);
    }

    [Authorize(Roles = SystemRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStadiumRequest request, CancellationToken cancellationToken)
    {
        var result = await _stadiumService.CreateAsync(
            request,
            cancellationToken);

        return HandleResult(result);
    }

    [Authorize(Roles = SystemRoles.Admin)]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateStadiumRequest request, CancellationToken cancellationToken)
    {
        var result = await _stadiumService.UpdateAsync(
            id,
            request,
            cancellationToken);

        return HandleResult(result);
    }

    [Authorize(Roles = SystemRoles.Admin)]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await _stadiumService.DeleteAsync(id, cancellationToken);

        return HandleResult(result);
    }
}