USE DIP_MasterData
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PatternCatalogs] ADD [Type] int NOT NULL DEFAULT 0;
GO

CREATE UNIQUE INDEX [IX_PatternCatalogs_TenantId_Key_Type] ON [PatternCatalogs] ([TenantId], [Key], [Type]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260128113045_add-type-to-pattern-catalog', N'8.0.21');
GO

COMMIT;
GO

