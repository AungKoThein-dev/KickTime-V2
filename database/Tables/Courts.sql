Use KickTime;

CREATE TABLE Courts
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,

    StadiumId BIGINT NOT NULL,

    Name NVARCHAR(100) NOT NULL,

    CourtType NVARCHAR(50) NOT NULL,

    PricePerHour DECIMAL(10,2) NOT NULL,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Courts_Stadiums
        FOREIGN KEY(StadiumId)
        REFERENCES Stadiums(Id)
);