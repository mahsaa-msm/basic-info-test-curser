USE DIP_MasterData 
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Cities] DROP CONSTRAINT [FK_Cities_Provinces_TenantId_ProvinceCoreId];
GO

ALTER TABLE [InsuranceUnits] DROP CONSTRAINT [FK_InsuranceUnits_Cities_TenantId_CityCoreId];
GO

ALTER TABLE [Provinces] DROP CONSTRAINT [FK_Provinces_Countries_TenantId_CountryCoreId];
GO

ALTER TABLE [Provinces] DROP CONSTRAINT [AK_Provinces_TenantId_CoreId];
GO

DROP INDEX [IX_Provinces_TenantId_CountryCoreId] ON [Provinces];
GO

DROP INDEX [IX_InsuranceUnits_TenantId_CityCoreId] ON [InsuranceUnits];
GO

ALTER TABLE [Countries] DROP CONSTRAINT [AK_Countries_TenantId_CoreId];
GO

ALTER TABLE [Cities] DROP CONSTRAINT [AK_Cities_TenantId_CoreId];
GO

DROP INDEX [IX_Cities_TenantId_ProvinceCoreId] ON [Cities];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260125114620_revert-aggregate-relations', N'8.0.21');
GO

COMMIT;
GO

