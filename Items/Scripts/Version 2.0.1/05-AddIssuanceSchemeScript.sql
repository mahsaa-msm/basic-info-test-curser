USE DIP_MasterData
GO
BEGIN TRANSACTION;
GO

CREATE TABLE [IssuanceSchemes] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(350) NOT NULL,
    [DisplayTitle] nvarchar(350) NOT NULL,
    [CoreId] nvarchar(50) NOT NULL,
    [Code] nvarchar(50) NOT NULL,
    [Priority] bigint NOT NULL,
    [IsActive] bit NOT NULL,
    [FromStartDateUtc] datetime2 NULL,
    [ToStartDateUtc] datetime2 NULL,
    [FromIssueDateUtc] datetime2 NULL,
    [ToIssueDateUtc] datetime2 NULL,
    [InsuranceTypeCoreId] nvarchar(50) NOT NULL,
    [AdjustmentType] int NULL,
    [AdjustmentPercent] float NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    [TenantId] bigint NOT NULL,
    [TenantBusinessId] uniqueidentifier NULL,
    CONSTRAINT [PK_IssuanceSchemes] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE INDEX [IX_IssuanceSchemes_BusinessId] ON [IssuanceSchemes] ([BusinessId]);
GO

CREATE INDEX [IX_IssuanceSchemes_CoreId] ON [IssuanceSchemes] ([CoreId]);
GO

CREATE INDEX [IX_IssuanceSchemes_InsuranceTypeCoreId] ON [IssuanceSchemes] ([InsuranceTypeCoreId]);
GO

CREATE UNIQUE INDEX [IX_IssuanceSchemes_TenantId_CoreId] ON [IssuanceSchemes] ([TenantId], [CoreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260128103921_add-issuance-scheme', N'8.0.21');
GO

COMMIT;
GO

