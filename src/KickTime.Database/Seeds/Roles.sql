IF NOT EXISTS
(
    SELECT 1
    FROM dbo.Roles
    WHERE Name = 'Admin'
)
BEGIN
    INSERT INTO dbo.Roles
    (
        Name,
        Description
    )
    VALUES
    (
        'Admin',
        'System Administrator'
    );
END;

IF NOT EXISTS
(
    SELECT 1
    FROM dbo.Roles
    WHERE Name = 'User'
)
BEGIN
    INSERT INTO dbo.Roles
    (
        Name,
        Description
    )
    VALUES
    (
        'User',
        'Regular Player'
    );
END;