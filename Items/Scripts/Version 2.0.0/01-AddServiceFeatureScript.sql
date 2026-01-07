USE DIP_MasterData
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ServiceFeatures] (
    [Id] bigint NOT NULL IDENTITY,
    [ServiceName] nvarchar(250) NOT NULL,
    [FeatureName] nvarchar(250) NOT NULL,
    [Key] int NOT NULL,
    [Description] nvarchar(500) NULL,
    [IsActive] bit NOT NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    [TenantId] bigint NOT NULL,
    [TenantBusinessId] uniqueidentifier NULL,
    CONSTRAINT [PK_ServiceFeatures] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE INDEX [IX_ServiceFeatures_BusinessId] ON [ServiceFeatures] ([BusinessId]);
GO

CREATE INDEX [IX_ServiceFeatures_Key] ON [ServiceFeatures] ([Key]);
GO

CREATE UNIQUE INDEX [IX_ServiceFeatures_TenantId_Key] ON [ServiceFeatures] ([TenantId], [Key]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260107074751_service-feature-entity', N'8.0.21');
GO

COMMIT;
GO

