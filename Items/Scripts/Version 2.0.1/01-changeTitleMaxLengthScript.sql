USE DIP_MasterData
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Provinces]') AND [c].[name] = N'Title');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Provinces] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Provinces] ALTER COLUMN [Title] nvarchar(350) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Provinces]') AND [c].[name] = N'DisplayTitle');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Provinces] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Provinces] ALTER COLUMN [DisplayTitle] nvarchar(350) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InsuranceUnits]') AND [c].[name] = N'Title');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [InsuranceUnits] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [InsuranceUnits] ALTER COLUMN [Title] nvarchar(350) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InsuranceUnits]') AND [c].[name] = N'Name');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [InsuranceUnits] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [InsuranceUnits] ALTER COLUMN [Name] nvarchar(350) NOT NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InsuranceUnits]') AND [c].[name] = N'DisplayTitle');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [InsuranceUnits] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [InsuranceUnits] ALTER COLUMN [DisplayTitle] nvarchar(350) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Countries]') AND [c].[name] = N'Title');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Countries] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Countries] ALTER COLUMN [Title] nvarchar(350) NOT NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Countries]') AND [c].[name] = N'DisplayTitle');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Countries] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Countries] ALTER COLUMN [DisplayTitle] nvarchar(350) NOT NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cities]') AND [c].[name] = N'Title');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Cities] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Cities] ALTER COLUMN [Title] nvarchar(350) NOT NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cities]') AND [c].[name] = N'DisplayTitle');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Cities] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Cities] ALTER COLUMN [DisplayTitle] nvarchar(350) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260122164155_change-titles-max-length', N'8.0.21');
GO

COMMIT;
GO
