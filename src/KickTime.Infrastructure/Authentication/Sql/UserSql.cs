using System;
using System.Collections.Generic;
using System.Text;

namespace KickTime.Infrastructure.Authentication.Sql
{
    internal static class UserSql
    {
        public const string GetById = """
        SELECT
            Id,
            Name,
            Email,
            Phone,
            PasswordHash,
            RoleId,
            IsActive,
            CreatedAt,
            UpdatedAt
        FROM Users
        WHERE Id = @Id;
        """;

        public const string GetByEmail = """
        SELECT
            Id,
            Name,
            Email,
            Phone,
            PasswordHash,
            RoleId,
            IsActive,
            CreatedAt,
            UpdatedAt
        FROM Users
        WHERE Email = @Email;
        """;

        public const string ExistsByEmail = """
        SELECT COUNT(1)
        FROM Users
        WHERE Email = @Email;
        """;

        public const string Insert = """
        INSERT INTO Users
        (
            Name,
            Email,
            Phone,
            PasswordHash,
            RoleId,
            IsActive
        )
        VALUES
        (
            @Name,
            @Email,
            @Phone,
            @PasswordHash,
            @RoleId,
            @IsActive
        );

        SELECT CAST(SCOPE_IDENTITY() AS BIGINT);
        """;

        public const string Update = """
        UPDATE Users
        SET
            Name = @Name,
            Phone = @Phone,
            RoleId = @RoleId,
            IsActive = @IsActive,
            UpdatedAt = SYSUTCDATETIME()
        WHERE Id = @Id;
        """;
    }
}
