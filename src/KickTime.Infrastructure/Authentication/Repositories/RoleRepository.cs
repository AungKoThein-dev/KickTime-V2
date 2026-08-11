using Dapper;
using KickTime.Application.Authentication.Interfaces;
using KickTime.Core.Entities;
using KickTime.Infrastructure.Authentication.Sql;
using KickTime.Infrastructure.Database;

namespace KickTime.Infrastructure.Authentication.Repositories
{
    public sealed class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public async Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            using var connection = await GetOpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
                RoleSql.GetById,
                new { Id = id },
                cancellationToken: cancellationToken);

            return await connection.QuerySingleOrDefaultAsync<Role>(command);
        }

        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            using var connection = await GetOpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
                RoleSql.GetByName,
                new { Name = name },
                cancellationToken: cancellationToken);

            return await connection.QuerySingleOrDefaultAsync<Role>(command);
        }
    }
}

