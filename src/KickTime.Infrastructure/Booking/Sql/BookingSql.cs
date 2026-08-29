namespace KickTime.Infrastructure.Booking.Sql;

public static class BookingSql
{
    public const string CreateIfAvailable = """
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

    BEGIN TRANSACTION;

    IF EXISTS
    (
        SELECT 1
        FROM Bookings WITH (UPDLOCK, HOLDLOCK)
        WHERE CourtId = @CourtId
          AND IsCancelled = 0
          AND StartTime < @EndTime
          AND EndTime > @StartTime
    )
    BEGIN
        ROLLBACK TRANSACTION;

        SELECT CAST(NULL AS BIGINT);
        RETURN;
    END;

    DECLARE @BookingId BIGINT;

    INSERT INTO Bookings
    (
        UserId,
        CourtId,
        StartTime,
        EndTime,
        IsCancelled
    )
    VALUES
    (
        @UserId,
        @CourtId,
        @StartTime,
        @EndTime,
        0
    );

    SET @BookingId = SCOPE_IDENTITY();

    COMMIT TRANSACTION;

    SELECT @BookingId;
    """;

    public const string GetById = """
        SELECT
            Id,
            UserId,
            CourtId,
            StartTime,
            EndTime,
            IsCancelled
        FROM Bookings
        WHERE Id = @Id;
        """;

    public const string GetByUserId = """
        SELECT
            Id,
            UserId,
            CourtId,
            StartTime,
            EndTime,
            IsCancelled
        FROM Bookings
        WHERE UserId = @UserId
        ORDER BY StartTime DESC;
        """;

    public const string GetByCourtId = """
        SELECT
            Id,
            UserId,
            CourtId,
            StartTime,
            EndTime,
            IsCancelled
        FROM Bookings
        WHERE CourtId = @CourtId
        ORDER BY StartTime;
        """;

    public const string HasOverlap = """
        SELECT CASE
            WHEN EXISTS
            (
                SELECT 1
                FROM Bookings
                WHERE CourtId = @CourtId
                  AND IsCancelled = 0
                  AND StartTime < @EndTime
                  AND EndTime > @StartTime
            )
            THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END;
        """;

    public const string Cancel = """
        UPDATE Bookings
        SET IsCancelled = 1
        WHERE Id = @Id
          AND IsCancelled = 0;
        """;
}