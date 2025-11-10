USE DIP_MasterData
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'zamin') IS NULL EXEC(N'CREATE SCHEMA [zamin];');
GO

CREATE TABLE [zamin].[OutBoxEventItems] (
    [OutBoxEventItemId] bigint NOT NULL IDENTITY,
    [EventId] uniqueidentifier NOT NULL,
    [AccuredByUserId] nvarchar(255) NOT NULL,
    [AccuredOn] datetime2 NOT NULL,
    [AggregateName] nvarchar(255) NOT NULL,
    [AggregateTypeName] nvarchar(500) NOT NULL,
    [AggregateId] nvarchar(max) NOT NULL,
    [EventName] nvarchar(255) NOT NULL,
    [EventTypeName] nvarchar(500) NOT NULL,
    [EventPayload] nvarchar(max) NOT NULL,
    [TraceId] nvarchar(100) NULL,
    [SpanId] nvarchar(100) NULL,
    [IsProcessed] bit NOT NULL,
    CONSTRAINT [PK_OutBoxEventItems] PRIMARY KEY ([OutBoxEventItemId])
);
GO

CREATE TABLE [Tenants] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantKey] uniqueidentifier NOT NULL,
    [Name] nvarchar(250) NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedDateUtc] datetime2 NOT NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Tenants] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TenantConfig] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] bigint NOT NULL,
    [ConfigType] int NOT NULL,
    [LastModifiedDateUtc] datetime2 NOT NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [SettingsJson] nvarchar(max) NOT NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TenantConfig] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TenantConfig_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TenantConfigSettingsHistory] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantConfigId] bigint NOT NULL,
    [OldSettings] nvarchar(max) NULL,
    [NewSettings] nvarchar(max) NOT NULL,
    [ChangeDateUtc] datetime2 NOT NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TenantConfigSettingsHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TenantConfigSettingsHistory_TenantConfig_TenantConfigId] FOREIGN KEY ([TenantConfigId]) REFERENCES [TenantConfig] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_TenantConfig_TenantId_ConfigType] ON [TenantConfig] ([TenantId], [ConfigType]);
GO

CREATE INDEX [IX_TenantConfigSettingsHistory_TenantConfigId] ON [TenantConfigSettingsHistory] ([TenantConfigId]);
GO

CREATE INDEX [IX_Tenants_Name] ON [Tenants] ([Name]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250819132236_add-tenant-agg', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Countries] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(250) NOT NULL,
    [DisplayTitle] nvarchar(250) NOT NULL,
    [CoreId] nvarchar(50) NOT NULL,
    [Code] nvarchar(50) NOT NULL,
    [Priority] int NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    [TenantId] bigint NOT NULL,
    [TenantBusinessId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Countries_BusinessId] ON [Countries] ([BusinessId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250819225502_add-country-agg', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Countries]') AND [c].[name] = N'TenantBusinessId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Countries] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Countries] ALTER COLUMN [TenantBusinessId] uniqueidentifier NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250819232900_fix-base-tenant-ids', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Countries]') AND [c].[name] = N'Priority');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Countries] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Countries] ALTER COLUMN [Priority] bigint NOT NULL;
GO

ALTER TABLE [Countries] ADD CONSTRAINT [AK_Countries_TenantId_CoreId] UNIQUE ([TenantId], [CoreId]);
GO

CREATE TABLE [ParrotTranslations] (
    [Id] bigint NOT NULL IDENTITY,
    [Key] nvarchar(200) NOT NULL,
    [Value] nvarchar(200) NOT NULL,
    [Culture] nvarchar(5) NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    [TenantId] bigint NOT NULL,
    [TenantBusinessId] uniqueidentifier NULL,
    CONSTRAINT [PK_ParrotTranslations] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Provinces] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(250) NOT NULL,
    [DisplayTitle] nvarchar(250) NOT NULL,
    [CoreId] nvarchar(50) NOT NULL,
    [CountryCoreId] nvarchar(50) NOT NULL,
    [Code] nvarchar(50) NOT NULL,
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
    CONSTRAINT [PK_Provinces] PRIMARY KEY ([Id]),
    CONSTRAINT [AK_Provinces_TenantId_CoreId] UNIQUE ([TenantId], [CoreId]),
    CONSTRAINT [FK_Provinces_Countries_TenantId_CountryCoreId] FOREIGN KEY ([TenantId], [CountryCoreId]) REFERENCES [Countries] ([TenantId], [CoreId])
);
GO

CREATE TABLE [Cities] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(250) NOT NULL,
    [DisplayTitle] nvarchar(250) NOT NULL,
    [CoreId] nvarchar(50) NOT NULL,
    [ProvinceCoreId] nvarchar(50) NOT NULL,
    [Code] nvarchar(50) NOT NULL,
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
    CONSTRAINT [PK_Cities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cities_Provinces_TenantId_ProvinceCoreId] FOREIGN KEY ([TenantId], [ProvinceCoreId]) REFERENCES [Provinces] ([TenantId], [CoreId])
);
GO

CREATE UNIQUE INDEX [IX_Countries_TenantId_CoreId] ON [Countries] ([TenantId], [CoreId]);
GO

CREATE UNIQUE INDEX [IX_Cities_BusinessId] ON [Cities] ([BusinessId]);
GO

CREATE INDEX [IX_Cities_CoreId] ON [Cities] ([CoreId]);
GO

CREATE INDEX [IX_Cities_ProvinceCoreId] ON [Cities] ([ProvinceCoreId]);
GO

CREATE UNIQUE INDEX [IX_Cities_TenantId_CoreId] ON [Cities] ([TenantId], [CoreId]);
GO

CREATE INDEX [IX_Cities_TenantId_ProvinceCoreId] ON [Cities] ([TenantId], [ProvinceCoreId]);
GO

CREATE UNIQUE INDEX [IX_Provinces_BusinessId] ON [Provinces] ([BusinessId]);
GO

CREATE INDEX [IX_Provinces_CoreId] ON [Provinces] ([CoreId]);
GO

CREATE INDEX [IX_Provinces_CountryCoreId] ON [Provinces] ([CountryCoreId]);
GO

CREATE UNIQUE INDEX [IX_Provinces_TenantId_CoreId] ON [Provinces] ([TenantId], [CoreId]);
GO

CREATE INDEX [IX_Provinces_TenantId_CountryCoreId] ON [Provinces] ([TenantId], [CountryCoreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251110055700_add-country-province-city-entity', N'8.0.1');
GO

COMMIT;
GO

