USE DIP_MasterData
GO
BEGIN TRANSACTION;
GO

CREATE TABLE [AgreementObligations] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(250) NOT NULL,
    [DisplayTitle] nvarchar(250) NOT NULL,
    [CoreId] nvarchar(50) NOT NULL,
    [AgreementCoreId] nvarchar(50) NOT NULL,
    [Code] nvarchar(50) NOT NULL,
    [StartDateUtc] datetime2 NULL,
    [EndDateUtc] datetime2 NULL,
    [PrepaymentPercentage] float NULL,
    [FirstInstallmentDeadline] int NULL,
    [InstallmentsCount] int NULL,
    [InstallmentInterval] int NULL,
    [AgreementObligationNumber] nvarchar(100) NOT NULL,
    [AgreementNumber] nvarchar(100) NOT NULL,
    [InsuranceTypeCoreId] nvarchar(50) NOT NULL,
    [SalesType] int NOT NULL,
    [Priority] bigint NOT NULL,
    [IsActive] bit NOT NULL,
    [IssuanceSchemeCoreIds] nvarchar(2000) NOT NULL,
    [CreatedByUserId] nvarchar(50) NULL,
    [CreatedDateTime] datetime2 NULL,
    [ModifiedByUserId] nvarchar(50) NULL,
    [ModifiedDateTime] datetime2 NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    [TenantId] bigint NOT NULL,
    [TenantBusinessId] uniqueidentifier NULL,
    CONSTRAINT [PK_AgreementObligations] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_AgreementObligations_AgreementCoreId] ON [AgreementObligations] ([AgreementCoreId]);
GO

CREATE UNIQUE INDEX [IX_AgreementObligations_BusinessId] ON [AgreementObligations] ([BusinessId]);
GO

CREATE INDEX [IX_AgreementObligations_CoreId] ON [AgreementObligations] ([CoreId]);
GO

CREATE INDEX [IX_AgreementObligations_InsuranceTypeCoreId] ON [AgreementObligations] ([InsuranceTypeCoreId]);
GO

CREATE UNIQUE INDEX [IX_AgreementObligations_TenantId_CoreId] ON [AgreementObligations] ([TenantId], [CoreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260120102707_AddAgreementObligation', N'8.0.21');
GO

COMMIT;
GO

