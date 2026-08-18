using KickTime.API.Controller;
using KickTime.Application.Booking.Interfaces;
using KickTime.Application.Common.Interfaces;
using KickTime.Core.DTOs.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KickTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class BookingController : BaseController
{
    private readonly IBookingService _bookingService;
    private readonly ICurrentUserService _currentUserService;

    public BookingController(
    IBookingService bookingService,
    ICurrentUserService currentUserService)
    {
        _bookingService =
            bookingService
            ?? throw new ArgumentNullException(nameof(bookingService));

        _currentUserService =
            currentUserService
            ?? throw new ArgumentNullException(nameof(currentUserService));
    }

   [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateBookingRequest request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId is null)
        {
            return Unauthorized("User id claim is missing.");
        }

        var result = await _bookingService.CreateAsync(
            userId.Value,
            request,
            cancellationToken);

        return HandleResult(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        long id,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId is null)
        {
            return Unauthorized("User id claim is missing.");
        }

        var result = await _bookingService.GetByIdAsync(
            id,
            userId.Value,
            cancellationToken);

        return HandleResult(result);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyBookings(
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId is null)
        {
            return Unauthorized("User id claim is missing.");
        }

        var result = await _bookingService.GetMyBookingsAsync(
            userId.Value,
            cancellationToken);

        return HandleResult(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Cancel(
        long id,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId is null)
        {
            return Unauthorized("User id claim is missing.");
        }

        var result = await _bookingService.CancelAsync(
            id,
            userId.Value,
            cancellationToken);

        return HandleResult(result);
    }
}