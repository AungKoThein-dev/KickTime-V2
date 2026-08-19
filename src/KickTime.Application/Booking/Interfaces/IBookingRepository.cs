using KickTime.Core.Entities;

namespace KickTime.Application.Booking.Interfaces;

public interface IBookingRepository
{
    Task<long?> CreateIfAvailableAsync(
    BookingEntity booking,
    CancellationToken cancellationToken);

    Task<BookingEntity?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken);

    Task<IEnumerable<BookingEntity>> GetByUserIdAsync(
        long userId,
        CancellationToken cancellationToken);

    Task<IEnumerable<BookingEntity>> GetByCourtIdAsync(
        long courtId,
        CancellationToken cancellationToken);

    Task<bool> HasOverlapAsync(
        long courtId,
        DateTime startTime,
        DateTime endTime,
        CancellationToken cancellationToken);

    Task<bool> CancelAsync(
        long id,
        CancellationToken cancellationToken);
}