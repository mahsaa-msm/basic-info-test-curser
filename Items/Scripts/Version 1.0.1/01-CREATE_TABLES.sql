USE DIP_MasterData
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Cities] ADD CONSTRAINT [AK_Cities_TenantId_CoreId] UNIQUE ([TenantId], [CoreId]);
GO

CREATE TABLE [InsuranceUnits] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(250) NOT NULL,
    [Title] nvarchar(250) NOT NULL,
    [DisplayTitle] nvarchar(250) NOT NULL,
    [CoreId] nvarchar(50) NOT NULL,
    [CityCoreId] nvarchar(50) NOT NULL,
    [Code] nvarchar(50) NOT NULL,
    [Location] geometry NULL,
    [Type] int NOT NULL,
    [State] int NOT NULL,
    [Priority] bigint NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    [TenantId] bigint NOT NULL,
    [TenantBusinessId] uniqueidentifier NULL,
    CONSTRAINT [PK_InsuranceUnits] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InsuranceUnits_Cities_TenantId_CityCoreId] FOREIGN KEY ([TenantId], [CityCoreId]) REFERENCES [Cities] ([TenantId], [CoreId])
);
GO

CREATE UNIQUE INDEX [IX_InsuranceUnits_BusinessId] ON [InsuranceUnits] ([BusinessId]);
GO

CREATE INDEX [IX_InsuranceUnits_CityCoreId] ON [InsuranceUnits] ([CityCoreId]);
GO

CREATE INDEX [IX_InsuranceUnits_CoreId] ON [InsuranceUnits] ([CoreId]);
GO


                CREATE SPATIAL INDEX [IX_InsuranceUnits_Location_Spatial] 
                ON [InsuranceUnits] ([Location])
                WITH (BOUNDING_BOX = (-180, -90, 180, 90))
GO

CREATE INDEX [IX_InsuranceUnits_TenantId_CityCoreId] ON [InsuranceUnits] ([TenantId], [CityCoreId]);
GO

CREATE UNIQUE INDEX [IX_InsuranceUnits_TenantId_CoreId] ON [InsuranceUnits] ([TenantId], [CoreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251126083319_insurance-unit-entity', N'8.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [PatternCatalogs] (
    [Id] bigint NOT NULL IDENTITY,
    [Key] nvarchar(50) NOT NULL,
    [Pattern] nvarchar(250) NOT NULL,
    [Description] nvarchar(500) NULL,
    [CreatedDateUtc] datetime2 NOT NULL,
    [LastModifiedDateUtc] datetime2 NULL,
    [Priority] bigint NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    [TenantId] bigint NOT NULL,
    [TenantBusinessId] uniqueidentifier NULL,
    CONSTRAINT [PK_PatternCatalogs] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE INDEX [IX_PatternCatalogs_BusinessId] ON [PatternCatalogs] ([BusinessId]);
GO

CREATE INDEX [IX_PatternCatalogs_Key] ON [PatternCatalogs] ([Key]);
GO

CREATE UNIQUE INDEX [IX_PatternCatalogs_TenantId_Id] ON [PatternCatalogs] ([TenantId], [Id]);
GO

CREATE UNIQUE INDEX [IX_PatternCatalogs_TenantId_Key] ON [PatternCatalogs] ([TenantId], [Key]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251208080937_add-pattern-catalog-entity', N'8.0.21');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PatternCatalogs]') AND [c].[name] = N'Pattern');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PatternCatalogs] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [PatternCatalogs] ALTER COLUMN [Pattern] nvarchar(500) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251209104119_change-pattern-config', N'8.0.21');
GO

COMMIT;
GO

