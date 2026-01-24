USE DIP_MasterData
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [InsuranceTypes] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(350) NOT NULL,
    [DisplayTitle] nvarchar(350) NOT NULL,
    [CoreId] nvarchar(50) NOT NULL,
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
    CONSTRAINT [PK_InsuranceTypes] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE INDEX [IX_InsuranceTypes_BusinessId] ON [InsuranceTypes] ([BusinessId]);
GO

CREATE INDEX [IX_InsuranceTypes_CoreId] ON [InsuranceTypes] ([CoreId]);
GO

CREATE UNIQUE INDEX [IX_InsuranceTypes_TenantId_CoreId] ON [InsuranceTypes] ([TenantId], [CoreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260122164302_add-insurance-type-entity', N'8.0.21');
GO

COMMIT;
GO