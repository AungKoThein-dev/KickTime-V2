using System.Data.Common;

namespace KickTime.Infrastructure.Database;

public interface IDbConnectionFactory
{
    DbConnection CreateConnection();
}