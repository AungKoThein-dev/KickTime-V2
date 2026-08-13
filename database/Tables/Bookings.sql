CREATE TABLE Bookings
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,

    UserId BIGINT NOT NULL,

    CourtId BIGINT NOT NULL,

    BookingDate DATE NOT NULL,

    StartTime TIME NOT NULL,

    EndTime TIME NOT NULL,

    Status NVARCHAR(30) NOT NULL,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Bookings_Users
        FOREIGN KEY(UserId)
        REFERENCES Users(Id),

    CONSTRAINT FK_Bookings_Courts
        FOREIGN KEY(CourtId)
        REFERENCES Courts(Id)
);