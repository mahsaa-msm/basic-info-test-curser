BEGIN TRANSACTION;
GO

DROP INDEX [IX_ServiceFeatures_Key] ON [ServiceFeatures];
DROP INDEX [IX_ServiceFeatures_TenantId_Key] ON [ServiceFeatures];
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceFeatures]') AND [c].[name] = N'Key');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ServiceFeatures] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ServiceFeatures] ALTER COLUMN [Key] bigint NOT NULL;
CREATE INDEX [IX_ServiceFeatures_Key] ON [ServiceFeatures] ([Key]);
CREATE UNIQUE INDEX [IX_ServiceFeatures_TenantId_Key] ON [ServiceFeatures] ([TenantId], [Key]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260411130616_service-feature-key-type', N'8.0.21');
GO

COMMIT;
GO

