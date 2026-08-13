namespace KickTime.Infrastructure.Stadium.Sql;

internal static class StadiumSql
{
    internal const string Create = """
        INSERT INTO Stadiums
        (
            Name,
            Location,
            Description
        )
        OUTPUT INSERTED.Id
        VALUES
        (
            @Name,
            @Location,
            @Description
        );
        """;

    internal const string GetById = """
        SELECT
            Id,
            Name,
            Location,
            Description,
            CreatedAt
        FROM Stadiums
        WHERE Id = @Id;
        """;

    internal const string GetAll = """
        SELECT
            Id,
            Name,
            Location,
            Description,
            CreatedAt
        FROM Stadiums
        ORDER BY Name;
        """;

    internal const string Update = """
        UPDATE Stadiums
        SET
            Name = @Name,
            Location = @Location,
            Description = @Description
        WHERE Id = @Id;
        """;

    internal const string Delete = """
        DELETE FROM Stadiums
        WHERE Id = @Id;
        """;
}