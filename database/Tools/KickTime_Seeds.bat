@ECHO OFF

SETLOCAL

SET DB_NAME=%1
SET SERVER_NAME=%2
SET USERNAME=%3
SET PASSWORD=%4

ECHO DB_Name = %DB_NAME%

@ECHO --------------------------------------------
@ECHO Insert Data.
@ECHO --------------------------------------------

PUSHD "../"
PUSHD "./Seeds"

@ECHO * Seed (Roles)
sqlcmd -S %SERVER_NAME% -U %USERNAME% -P %PASSWORD% -f 932 -i "Roles.sql" -v dbName=%DB_NAME%

@ECHO --------------------------------------------
@ECHO Done.