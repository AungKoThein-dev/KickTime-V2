using Dapper;
using KickTime.Application.Booking.Interfaces;
using KickTime.Core.Entities;
using KickTime.Infrastructure.Booking.Sql;
using KickTime.Infrastructure.Database;

namespace KickTime.Infrastructure.Booking.Repositories;

public sealed class BookingRepository : BaseRepository, IBookingRepository
{
    public BookingRepository(
        IDbConnectionFactory connectionFactory)
        : base(connectionFactory)
    {
    }

    public async Task<long> CreateAsync(
        BookingEntity booking,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            BookingSql.Create,
            new
            {
                booking.UserId,
                booking.CourtId,
                booking.StartTime,
                booking.EndTime
            },
            cancellationToken: cancellationToken);

        return await connection.ExecuteScalarAsync<long>(command);
    }

    public async Task<BookingEntity?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            BookingSql.GetById,
            new { Id = id },
            cancellationToken: cancellationToken);

        return await connection.QuerySingleOrDefaultAsync<BookingEntity>(
            command);
    }

    public async Task<IEnumerable<BookingEntity>> GetByUserIdAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            BookingSql.GetByUserId,
            new { UserId = userId },
            cancellationToken: cancellationToken);

        return await connection.QueryAsync<BookingEntity>(command);
    }

    public async Task<IEnumerable<BookingEntity>> GetByCourtIdAsync(
        long courtId,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            BookingSql.GetByCourtId,
            new { CourtId = courtId },
            cancellationToken: cancellationToken);

        return await connection.QueryAsync<BookingEntity>(command);
    }

    public async Task<bool> HasOverlapAsync(
        long courtId,
        DateTime startTime,
        DateTime endTime,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            BookingSql.HasOverlap,
            new
            {
                CourtId = courtId,
                StartTime = startTime,
                EndTime = endTime
            },
            cancellationToken: cancellationToken);

        return await connection.ExecuteScalarAsync<bool>(command);
    }

    public async Task<bool> CancelAsync(
        long id,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            BookingSql.Cancel,
            new { Id = id },
            cancellationToken: cancellationToken);

        var affectedRows =
            await connection.ExecuteAsync(command);

        return affectedRows > 0;
    }
}