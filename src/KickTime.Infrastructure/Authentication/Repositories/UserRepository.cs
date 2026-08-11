using Dapper;
using KickTime.Application.Authentication.Interfaces;
using KickTime.Core.Entities;
using KickTime.Infrastructure.Authentication.Sql;
using KickTime.Infrastructure.Database;

namespace KickTime.Infrastructure.Authentication.Repositories
{
    public sealed class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            using var connection = await GetOpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
            UserSql.GetById,
            new { Id = id },
            cancellationToken: cancellationToken);

            return await connection.QuerySingleOrDefaultAsync<User>(command);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            using var connection = await GetOpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
                UserSql.GetByEmail,
                new { Email = email },
                cancellationToken: cancellationToken);

            return await connection.QuerySingleOrDefaultAsync<User>(command);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            using var connection = await GetOpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
                UserSql.ExistsByEmail,
                new { Email = email },
                cancellationToken: cancellationToken);

            var count = await connection.ExecuteScalarAsync<int>(command);

            return count > 0;
        }

        public async Task<long> CreateAsync(User user, CancellationToken cancellationToken)
        {
            using var connection = await GetOpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
                UserSql.Insert,
                user,
                cancellationToken: cancellationToken);

            return await connection.ExecuteScalarAsync<long>(command);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            using var connection = await GetOpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
                UserSql.Update,
                user,
                cancellationToken: cancellationToken);

            await connection.ExecuteAsync(command);
        }
    }
}


