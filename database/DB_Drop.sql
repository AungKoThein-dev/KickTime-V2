USE [master]
GO

IF EXISTS(select * from sys.databases where name='$(dbName)')
BEGIN
  ALTER DATABASE [$(dbName)] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
  DROP DATABASE [$(dbName)]
END
GO

