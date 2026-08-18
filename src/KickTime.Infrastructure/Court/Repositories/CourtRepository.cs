using Dapper;
using KickTime.Core.Entities;
using KickTime.Application.Court.Interfaces;
using KickTime.Infrastructure.Database;
using KickTime.Infrastructure.Court.Sql;

namespace KickTime.Infrastructure.Court.Repositories;

public sealed class CourtRepository : BaseRepository, ICourtRepository
{
    public CourtRepository(
        IDbConnectionFactory connectionFactory)
        : base(connectionFactory)
    {
    }

    public async Task<long> CreateAsync(
        CourtEntity court,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            CourtSql.Create,
            new
            {
                court.StadiumId,
                court.Name,
                court.Description,
                court.IsActive
            },
            cancellationToken: cancellationToken);

        return await connection.ExecuteScalarAsync<long>(
            command);
    }

    public async Task<CourtEntity?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            CourtSql.GetById,
            new { Id = id },
            cancellationToken: cancellationToken);

        var temp = await connection.QuerySingleOrDefaultAsync<CourtEntity>(
            command);
        return await connection.QuerySingleOrDefaultAsync<CourtEntity>(
            command);
    }

    public async Task<IEnumerable<CourtEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            CourtSql.GetAll,
            cancellationToken: cancellationToken);

        return await connection.QueryAsync<CourtEntity>(
            command);
    }

    public async Task<IEnumerable<CourtEntity>> GetByStadiumIdAsync(
        long stadiumId,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            CourtSql.GetByStadiumId,
            new { StadiumId = stadiumId },
            cancellationToken: cancellationToken);

        return await connection.QueryAsync<CourtEntity>(
            command);
    }

    public async Task<bool> UpdateAsync(
        CourtEntity court,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            CourtSql.Update,
            new
            {
                court.Id,
                court.Name,
                court.Description,
                court.IsActive
            },
            cancellationToken: cancellationToken);

        var affectedRows =
            await connection.ExecuteAsync(command);

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(
        long id,
        CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            CourtSql.Delete,
            new { Id = id },
            cancellationToken: cancellationToken);

        var affectedRows =
            await connection.ExecuteAsync(command);

        return affectedRows > 0;
    }

    public async Task<bool> ExistsByNameAsync(
    long stadiumId,
    string name,
    CancellationToken cancellationToken)
    {
        using var connection =
            await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            CourtSql.ExistsByName,
            new
            {
                StadiumId = stadiumId,
                Name = name
            },
            cancellationToken: cancellationToken);

        return await connection.ExecuteScalarAsync<bool>(
            command);
    }
}