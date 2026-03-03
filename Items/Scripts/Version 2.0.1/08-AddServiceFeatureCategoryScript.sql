USE DIP_MasterData
GO
BEGIN TRANSACTION;
GO

ALTER TABLE [InsuranceTypes] ADD [ServiceFeatureCategory] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260227093009_add-service-feature-category-insurance-type', N'8.0.21');
GO

COMMIT;
GO

