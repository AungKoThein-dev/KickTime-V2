using System.Data;
using System.Data.Common;

namespace KickTime.Infrastructure.Database;

public abstract class BaseRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    protected BaseRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    protected async Task<DbConnection> GetOpenConnectionAsync(CancellationToken cancellationToken)
    {
        var connection = _connectionFactory.CreateConnection();

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }

        return connection;
    }
}