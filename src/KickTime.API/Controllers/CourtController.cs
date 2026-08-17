using KickTime.API.Controller;
using KickTime.Application.Court.Interfaces;
using KickTime.Core.DTOs.Court;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KickTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class CourtController : BaseController
{
    private readonly ICourtService _courtService;

    public CourtController(ICourtService courtService)
    {
        _courtService =
            courtService
            ?? throw new ArgumentNullException(nameof(courtService));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCourtRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _courtService.CreateAsync(
            request,
            cancellationToken);

        return HandleResult(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        long id,
        CancellationToken cancellationToken)
    {
        var result = await _courtService.GetByIdAsync(
            id,
            cancellationToken);

        return HandleResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _courtService.GetAllAsync(
            cancellationToken);

        return HandleResult(result);
    }

    [HttpGet("stadium/{stadiumId:long}")]
    public async Task<IActionResult> GetByStadiumId(
        long stadiumId,
        CancellationToken cancellationToken)
    {
        var result = await _courtService.GetByStadiumIdAsync(
            stadiumId,
            cancellationToken);

        return HandleResult(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] UpdateCourtRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _courtService.UpdateAsync(
            id,
            request,
            cancellationToken);

        return HandleResult(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        long id,
        CancellationToken cancellationToken)
    {
        var result = await _courtService.DeleteAsync(
            id,
            cancellationToken);

        return HandleResult(result);
    }
}