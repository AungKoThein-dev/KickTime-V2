using Dapper;
using KickTime.Application.Stadium.Interfaces;
using StadiumEntity = KickTime.Core.Entities.Stadium;
using KickTime.Infrastructure.Database;
using KickTime.Infrastructure.Stadium.Sql;

namespace KickTime.Infrastructure.Stadium.Repositories;

public sealed class StadiumRepository : BaseRepository, IStadiumRepository
{
    public StadiumRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
    {
    }

    public async Task<long> CreateAsync(
        StadiumEntity stadium,
        CancellationToken cancellationToken)
    {
        try
        {

            using var connection = await GetOpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
                StadiumSql.Create,
                new
                {
                    stadium.Name,
                    stadium.Location,
                    stadium.Description
                },
                cancellationToken: cancellationToken);

            return await connection.ExecuteScalarAsync<long>(command);
        }
        catch(Exception ex)
        {
            throw;
        }
        
    }

    public async Task<StadiumEntity?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken)
    {
        using var connection = await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            StadiumSql.GetById,
            new { Id = id },
            cancellationToken: cancellationToken);

        return await connection.QuerySingleOrDefaultAsync<StadiumEntity>(
            command);
    }

    public async Task<IEnumerable<StadiumEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        using var connection = await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            StadiumSql.GetAll,
            cancellationToken: cancellationToken);

        return await connection.QueryAsync<StadiumEntity>(command);
    }

    public async Task<bool> UpdateAsync(
        StadiumEntity stadium,
        CancellationToken cancellationToken)
    {
        using var connection = await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            StadiumSql.Update,
            new
            {
                stadium.Id,
                stadium.Name,
                stadium.Location,
                stadium.Description
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
        using var connection = await GetOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            StadiumSql.Delete,
            new { Id = id },
            cancellationToken: cancellationToken);

        var affectedRows =
            await connection.ExecuteAsync(command);

        return affectedRows > 0;
    }
}