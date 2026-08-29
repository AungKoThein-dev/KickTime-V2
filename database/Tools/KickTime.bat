@ECHO OFF

SETLOCAL

SET DB_NAME=KickTimeTest
SET SERVER_NAME=(localdb)\MSSQLLocalDB
SET USERNAME=root
SET PASSWORD=Superm@n01

@ECHO --------------------------------------------
@ECHO Database Initialize.
@ECHO --------------------------------------------

PUSHD "../"
@ECHO * DropDB.
sqlcmd -S %SERVER_NAME% -U %USERNAME% -P %PASSWORD% -i DB_Drop.sql -v dbName=%DB_NAME%

@ECHO * CreateDB.
sqlcmd -S %SERVER_NAME% -U %USERNAME% -P %PASSWORD% -i DB_Create.sql -v dbName=%DB_NAME%

@ECHO --------------------------------------------
@ECHO Tables Initialize.
@ECHO --------------------------------------------

PUSHD "./Tables"
@ECHO * CreateTable (Role)
sqlcmd -S %SERVER_NAME% -U %USERNAME% -P %PASSWORD% -i Roles.sql -v dbName=%DB_NAME%

@ECHO * CreateTable (User)
sqlcmd -S %SERVER_NAME% -U %USERNAME% -P %PASSWORD% -i Users.sql -v dbName=%DB_NAME%

@ECHO * CreateTable (Stadiums)
sqlcmd -S %SERVER_NAME% -U %USERNAME% -P %PASSWORD% -i Stadiums.sql -v dbName=%DB_NAME%

@ECHO * CreateTable (Courts)
sqlcmd -S %SERVER_NAME% -U %USERNAME% -P %PASSWORD% -i Courts.sql -v dbName=%DB_NAME%

@ECHO * CreateTable (Bookings)
sqlcmd -S %SERVER_NAME% -U %USERNAME% -P %PASSWORD% -i Bookings.sql -v dbName=%DB_NAME%

PAUSE