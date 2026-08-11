using KickTime.Core.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace KickTime.Infrastructure.Database;

public sealed class SqlServerConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    private const string ConnectionName = "DefaultConnection";
    public SqlServerConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString(ConnectionName)
            ?? throw new InvalidOperationException(Messages.ConnectionStringNotFound);
    }

    public DbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}