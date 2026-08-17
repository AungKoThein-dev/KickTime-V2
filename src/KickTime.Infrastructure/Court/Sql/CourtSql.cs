namespace KickTime.Infrastructure.Court.Sql;

public static class CourtSql
{
    public const string Create = """
        INSERT INTO Courts
        (
            StadiumId,
            Name,
            Description,
            IsActive,
            CreatedAt
        )
        OUTPUT INSERTED.Id
        VALUES
        (
            @StadiumId,
            @Name,
            @Description,
            @IsActive,
            SYSUTCDATETIME()
        );
        """;

    public const string GetById = """
        SELECT
            Id,
            StadiumId,
            Name,
            Description,
            IsActive,
            CreatedAt,
            UpdatedAt
        FROM Courts
        WHERE Id = @Id;
        """;

    public const string GetAll = """
        SELECT
            Id,
            StadiumId,
            Name,
            Description,
            IsActive,
            CreatedAt,
            UpdatedAt
        FROM Courts
        ORDER BY Name;
        """;

    public const string GetByStadiumId = """
        SELECT
            Id,
            StadiumId,
            Name,
            Description,
            IsActive,
            CreatedAt,
            UpdatedAt
        FROM Courts
        WHERE StadiumId = @StadiumId
        ORDER BY Name;
        """;

    public const string Update = """
        UPDATE Courts
        SET
            Name = @Name,
            Description = @Description,
            IsActive = @IsActive,
            UpdatedAt = SYSUTCDATETIME()
        WHERE Id = @Id;
        """;

    public const string Delete = """
        DELETE FROM Courts
        WHERE Id = @Id;
        """;

    public const string ExistsByName = """
    SELECT CASE
        WHEN EXISTS
        (
            SELECT 1
            FROM Courts
            WHERE StadiumId = @StadiumId
              AND Name = @Name
        )
        THEN CAST(1 AS BIT)
        ELSE CAST(0 AS BIT)
    END;
    """;
}