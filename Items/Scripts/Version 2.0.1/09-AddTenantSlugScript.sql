BEGIN TRANSACTION;
GO

ALTER TABLE [Tenants] ADD [Slug] nvarchar(63) NOT NULL DEFAULT N'';
GO

CREATE UNIQUE INDEX [IX_Tenants_Slug] ON [Tenants] ([Slug]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260407073313_add-tenant-slug', N'8.0.21');
GO

COMMIT;
GO

