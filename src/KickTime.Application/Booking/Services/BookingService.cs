using KickTime.Application.Authentication.Interfaces;
using KickTime.Application.Booking.Interfaces;
using KickTime.Application.Booking.Mappings;
using KickTime.Application.Court.Interfaces;
using KickTime.Application.Results;
using KickTime.Core.DTOs.Booking;
using Microsoft.Extensions.Logging;

namespace KickTime.Application.Booking.Services;

public sealed class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ICourtRepository _courtRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<BookingService> _logger;

    public BookingService(
        IBookingRepository bookingRepository,
        ICourtRepository courtRepository,
        IUserRepository userRepository,
        ILogger<BookingService> logger)
    {
        _bookingRepository =
            bookingRepository
            ?? throw new ArgumentNullException(nameof(bookingRepository));

        _courtRepository =
            courtRepository
            ?? throw new ArgumentNullException(nameof(courtRepository));

        _userRepository =
            userRepository
            ?? throw new ArgumentNullException(nameof(userRepository));

        _logger =
            logger
            ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<BookingResponse>> CreateAsync(
        long userId,
        CreateBookingRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            userId,
            cancellationToken);

        if (user is null)
        {
            return Result<BookingResponse>.Unauthorized(
                "User was not found.");
        }

        var court = await _courtRepository.GetByIdAsync(
            request.CourtId,
            cancellationToken);

        if (court is null)
        {
            return Result<BookingResponse>.NotFound(
                "Court was not found.");
        }

        if (!court.IsActive)
        {
            return Result<BookingResponse>.Error(
                "The selected court is not active.");
        }

        if (request.StartTime >= request.EndTime)
        {
            return Result<BookingResponse>.ValidationFailure(
                "Start time must be earlier than end time.");
        }

        var hasOverlap = await _bookingRepository.HasOverlapAsync(
            request.CourtId,
            request.StartTime,
            request.EndTime,
            cancellationToken);

        if (hasOverlap)
        {
            return Result<BookingResponse>.Conflict(
                "The selected court is already booked for the requested time.");
        }

        var booking = request.ToEntity(userId);

        booking.Id = await _bookingRepository.CreateAsync(
            booking,
            cancellationToken);

        _logger.LogInformation(
            "Booking {BookingId} created successfully for User {UserId} and Court {CourtId}.",
            booking.Id,
            userId,
            booking.CourtId);

        return Result<BookingResponse>.Success(
            booking.ToResponse(),
            "Booking created successfully.");
    }

    public async Task<Result<BookingResponse>> GetByIdAsync(
        long id,
        long userId,
        CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (booking is null)
        {
            return Result<BookingResponse>.NotFound(
                "Booking was not found.");
        }

        if (booking.UserId != userId)
        {
            return Result<BookingResponse>.Unauthorized(
                "You are not allowed to access this booking.");
        }

        return Result<BookingResponse>.Success(
            booking.ToResponse());
    }

    public async Task<Result<IEnumerable<BookingResponse>>> GetMyBookingsAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(
            userId,
            cancellationToken);

        var response = bookings
            .Select(booking => booking.ToResponse())
            .ToList();

        return Result<IEnumerable<BookingResponse>>.Success(
            response);
    }

    public async Task<Result<bool>> CancelAsync(
        long id,
        long userId,
        CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (booking is null)
        {
            return Result<bool>.NotFound(
                "Booking was not found.");
        }

        if (booking.UserId != userId)
        {
            return Result<bool>.Unauthorized(
                "You are not allowed to cancel this booking.");
        }

        if (booking.IsCancelled)
        {
            return Result<bool>.Error(
                "Booking is already cancelled.");
        }

        var cancelled = await _bookingRepository.CancelAsync(
            id,
            cancellationToken);

        if (!cancelled)
        {
            return Result<bool>.Error(
                "Booking could not be cancelled.");
        }

        _logger.LogInformation(
            "Booking {BookingId} cancelled by User {UserId}.",
            id,
            userId);

        return Result<bool>.Success(
            true,
            "Booking cancelled successfully.");
    }
}