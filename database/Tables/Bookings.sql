USE [$(dbName)]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Bookings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Bookings
GO

CREATE TABLE Bookings
(
    Id BIGINT IDENTITY(1,1) NOT NULL
        CONSTRAINT PK_Bookings PRIMARY KEY,

    UserId BIGINT NOT NULL,

    CourtId BIGINT NOT NULL,

    StartTime DATETIME2 NOT NULL,

    EndTime DATETIME2 NOT NULL,

    IsCancelled BIT NOT NULL
        CONSTRAINT DF_Bookings_IsCancelled DEFAULT (0),

    CreatedAt DATETIME2 NOT NULL
        CONSTRAINT DF_Bookings_CreatedAt DEFAULT (SYSUTCDATETIME()),

    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Bookings_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(Id),

    CONSTRAINT FK_Bookings_Courts
        FOREIGN KEY (CourtId)
        REFERENCES Courts(Id)
);

GO