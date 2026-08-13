USE [master]
GO

IF EXISTS(select * from sys.databases where name='$(dbName)')
BEGIN
  -- RDSで問題になるため、RDSで使用する場合は以下の行はコメントアウト
  ALTER DATABASE [$(dbName)] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
  DROP DATABASE [$(dbName)]
END
GO

