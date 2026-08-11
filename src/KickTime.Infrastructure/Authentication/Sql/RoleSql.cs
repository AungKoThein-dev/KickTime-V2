using System;
using System.Collections.Generic;
using System.Text;

namespace KickTime.Infrastructure.Authentication.Sql
{
    internal static class RoleSql
    {
        public const string GetById = """
        SELECT
            Id,
            Name,
            Description,
            CreatedAt
        FROM Roles
        WHERE Id = @Id;
        """;

        public const string GetByName = """
        SELECT
            Id,
            Name,
            Description,
            CreatedAt
        FROM Roles
        WHERE Name = @Name;
        """;
    }
}
