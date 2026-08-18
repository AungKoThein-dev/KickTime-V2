using KickTime.Application.Results;
using KickTime.Core.DTOs.Booking;

namespace KickTime.Application.Booking.Interfaces;

public interface IBookingService
{
    Task<Result<BookingResponse>> CreateAsync(
        long userId,
        CreateBookingRequest request,
        CancellationToken cancellationToken);

    Task<Result<BookingResponse>> GetByIdAsync(
        long id,
        long userId,
        CancellationToken cancellationToken);

    Task<Result<IEnumerable<BookingResponse>>> GetMyBookingsAsync(
        long userId,
        CancellationToken cancellationToken);

    Task<Result<bool>> CancelAsync(
        long id,
        long userId,
        CancellationToken cancellationToken);
}