INSERT INTO Users
(
    RoleId,
    Name,
    Email,
    PasswordHash
)
VALUES
(
    1,
    'System Admin',
    'admin@kicktime.com',
    '$2a$11$ReplaceWithBCryptHash'
);