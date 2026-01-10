USE [DIP_MasterData] 
GO 
-- Applying translations for all tenants 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'OVERPAIED' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'اضافه پرداخت شده', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '2b68795d-7189-4fde-a81e-fa9d2b2eb676', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'OVERPAIED' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'OVERPAIED', N'اضافه پرداخت شده', 'fa-IR', '0', GETDATE(), '2b68795d-7189-4fde-a81e-fa9d2b2eb676', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'OVERPAIED' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'اضافه پرداخت شده', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '41cc8380-d8c5-4542-b2dd-d0dec827df5d', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'OVERPAIED' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'OVERPAIED', N'اضافه پرداخت شده', 'en-US', '0', GETDATE(), '41cc8380-d8c5-4542-b2dd-d0dec827df5d', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'YES' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'بله', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'e21e1c90-490c-4a17-8b78-68fccb153439', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'YES' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'YES', N'بله', 'fa-IR', '0', GETDATE(), 'e21e1c90-490c-4a17-8b78-68fccb153439', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'YES' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'بله', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '42a8466d-3975-495b-bb69-a5ee20334082', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'YES' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'YES', N'بله', 'en-US', '0', GETDATE(), '42a8466d-3975-495b-bb69-a5ee20334082', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'NO' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'خیر', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'e36430ee-3711-455d-9c63-4b6a39e1b20a', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'NO' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'NO', N'خیر', 'fa-IR', '0', GETDATE(), 'e36430ee-3711-455d-9c63-4b6a39e1b20a', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'NO' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'خیر', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '5b20e73f-0577-4945-b190-6ae230d76a47', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'NO' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'NO', N'خیر', 'en-US', '0', GETDATE(), '5b20e73f-0577-4945-b190-6ae230d76a47', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'g', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '034c2e36-8dcf-4204-a6ae-b37107572a4b', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد', N'g', 'fa-IR', '0', GETDATE(), '034c2e36-8dcf-4204-a6ae-b37107572a4b', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'g', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '28fc7e8c-6e08-480d-ba1e-8390798abd15', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد', N'g', 'en-US', '0', GETDATE(), '28fc7e8c-6e08-480d-ba1e-8390798abd15', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'g', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'c82ce499-4f60-4df8-9d50-041f0f22a7a5', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد', N'g', 'fa-IR', '0', GETDATE(), 'c82ce499-4f60-4df8-9d50-041f0f22a7a5', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'g', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'ba4c67d8-bf51-4550-b17b-6a3391c3ea58', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد', N'g', 'en-US', '0', GETDATE(), 'ba4c67d8-bf51-4550-b17b-6a3391c3ea58', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'VALIDATION_ERROR_FACTOR_HAS_ANY_SUCCESS_PAYMENT' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '88843c49-98c0-487c-ad32-98005d9ed762', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'VALIDATION_ERROR_FACTOR_HAS_ANY_SUCCESS_PAYMENT' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'VALIDATION_ERROR_FACTOR_HAS_ANY_SUCCESS_PAYMENT', N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد', 'fa-IR', '0', GETDATE(), '88843c49-98c0-487c-ad32-98005d9ed762', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'VALIDATION_ERROR_FACTOR_HAS_ANY_SUCCESS_PAYMENT' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'b169664c-01b1-4cd7-b00e-39c27c074f26', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'VALIDATION_ERROR_FACTOR_HAS_ANY_SUCCESS_PAYMENT' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'VALIDATION_ERROR_FACTOR_HAS_ANY_SUCCESS_PAYMENT', N'فاکتور به شناسه {0} هیچ پرداخت موفقی ندارد', 'en-US', '0', GETDATE(), 'b169664c-01b1-4cd7-b00e-39c27c074f26', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'VALIDATION_ERROR_FACTOR_HAS_MORE_THAN_ONE_ITEMS' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'فاکتور از نوع پرداخت گروهی می باشد', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'd4eb2049-3ad0-4dbb-97ca-47ca7dba51d6', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'VALIDATION_ERROR_FACTOR_HAS_MORE_THAN_ONE_ITEMS' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'VALIDATION_ERROR_FACTOR_HAS_MORE_THAN_ONE_ITEMS', N'فاکتور از نوع پرداخت گروهی می باشد', 'fa-IR', '0', GETDATE(), 'd4eb2049-3ad0-4dbb-97ca-47ca7dba51d6', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'VALIDATION_ERROR_FACTOR_HAS_MORE_THAN_ONE_ITEMS' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'فاکتور از نوع پرداخت گروهی می باشد', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'c6a8527b-c9c6-4c90-a3d2-33c1a26db014', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'VALIDATION_ERROR_FACTOR_HAS_MORE_THAN_ONE_ITEMS' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'VALIDATION_ERROR_FACTOR_HAS_MORE_THAN_ONE_ITEMS', N'فاکتور از نوع پرداخت گروهی می باشد', 'en-US', '0', GETDATE(), 'c6a8527b-c9c6-4c90-a3d2-33c1a26db014', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'VALIDATION_ERROR_FACTOR_INCORECT_TYPE_FOR_THIS_ACTION' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'فاکتور از نوع {0} می باشد. نوع قابل قبول برای این عملیات {1} می باشد.', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'e868bb33-f378-42ae-92ae-6b7ce02f38e5', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'VALIDATION_ERROR_FACTOR_INCORECT_TYPE_FOR_THIS_ACTION' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'VALIDATION_ERROR_FACTOR_INCORECT_TYPE_FOR_THIS_ACTION', N'فاکتور از نوع {0} می باشد. نوع قابل قبول برای این عملیات {1} می باشد.', 'fa-IR', '0', GETDATE(), 'e868bb33-f378-42ae-92ae-6b7ce02f38e5', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'VALIDATION_ERROR_FACTOR_INCORECT_TYPE_FOR_THIS_ACTION' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'فاکتور از نوع {0} می باشد. نوع قابل قبول برای این عملیات {1} می باشد.', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'b79573be-0523-4c3e-ac52-769a0829cf94', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'VALIDATION_ERROR_FACTOR_INCORECT_TYPE_FOR_THIS_ACTION' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'VALIDATION_ERROR_FACTOR_INCORECT_TYPE_FOR_THIS_ACTION', N'فاکتور از نوع {0} می باشد. نوع قابل قبول برای این عملیات {1} می باشد.', 'en-US', '0', GETDATE(), 'b79573be-0523-4c3e-ac52-769a0829cf94', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'VALIDATION_ERROR_IMPERFECT_FACTOR_ITEM_DETAIL' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'اطلاعات فاکتور به شناسه {0} ناقص است', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '2b4082c6-8760-4673-a276-4b6632ba9f74', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'VALIDATION_ERROR_IMPERFECT_FACTOR_ITEM_DETAIL' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'VALIDATION_ERROR_IMPERFECT_FACTOR_ITEM_DETAIL', N'اطلاعات فاکتور به شناسه {0} ناقص است', 'fa-IR', '0', GETDATE(), '2b4082c6-8760-4673-a276-4b6632ba9f74', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'VALIDATION_ERROR_IMPERFECT_FACTOR_ITEM_DETAIL' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'اطلاعات فاکتور به شناسه {0} ناقص است', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '29c1f6d2-57b3-43f4-95ae-75a0382ebb08', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'VALIDATION_ERROR_IMPERFECT_FACTOR_ITEM_DETAIL' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'VALIDATION_ERROR_IMPERFECT_FACTOR_ITEM_DETAIL', N'اطلاعات فاکتور به شناسه {0} ناقص است', 'en-US', '0', GETDATE(), '29c1f6d2-57b3-43f4-95ae-75a0382ebb08', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'OLD_DUE_DATE_UTC' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'تاریخ سررسید قبلی', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'bf97dfb3-0752-4dcb-95e6-5193c8e42672', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'OLD_DUE_DATE_UTC' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'OLD_DUE_DATE_UTC', N'تاریخ سررسید قبلی', 'fa-IR', '0', GETDATE(), 'bf97dfb3-0752-4dcb-95e6-5193c8e42672', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'OLD_DUE_DATE_UTC' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'تاریخ سررسید قبلی', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'f08c6cba-0614-47b4-9c97-e8845e119e47', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'OLD_DUE_DATE_UTC' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'OLD_DUE_DATE_UTC', N'تاریخ سررسید قبلی', 'en-US', '0', GETDATE(), 'f08c6cba-0614-47b4-9c97-e8845e119e47', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'OLD_INSTALLMENT_NUMBER' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'شماره قسط قبلی', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '77ce97bc-c15e-4e23-a267-83df2d762453', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'OLD_INSTALLMENT_NUMBER' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'OLD_INSTALLMENT_NUMBER', N'شماره قسط قبلی', 'fa-IR', '0', GETDATE(), '77ce97bc-c15e-4e23-a267-83df2d762453', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'OLD_INSTALLMENT_NUMBER' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'شماره قسط قبلی', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '61e8673f-4dd4-492c-a849-c6027a71625e', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'OLD_INSTALLMENT_NUMBER' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'OLD_INSTALLMENT_NUMBER', N'شماره قسط قبلی', 'en-US', '0', GETDATE(), '61e8673f-4dd4-492c-a849-c6027a71625e', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'بیمه نامه عمر به شماره {0} هیچ قسط حق بیمه/وام قابل پرداختی ندارد', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '03d401de-1773-4553-bb6d-5ffc6e09858b', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE', N'بیمه نامه عمر به شماره {0} هیچ قسط حق بیمه/وام قابل پرداختی ندارد', 'fa-IR', '0', GETDATE(), '03d401de-1773-4553-bb6d-5ffc6e09858b', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'بیمه نامه عمر به شماره {0} هیچ قسط حق بیمه/وام قابل پرداختی ندارد', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'a29bc86f-706b-4c7d-80f4-237090b66f7c', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE', N'بیمه نامه عمر به شماره {0} هیچ قسط حق بیمه/وام قابل پرداختی ندارد', 'en-US', '0', GETDATE(), 'a29bc86f-706b-4c7d-80f4-237090b66f7c', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'بیمه نامه عمر به شماره {0} هیچ قسط حق بیمه/وام قابل پرداختی ندارد', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '20f4a4d9-5c7c-4b90-b4c9-3791d48568f1', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE', N'بیمه نامه عمر به شماره {0} هیچ قسط حق بیمه/وام قابل پرداختی ندارد', 'fa-IR', '0', GETDATE(), '20f4a4d9-5c7c-4b90-b4c9-3791d48568f1', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'بیمه نامه عمر به شماره {0} هیچ قسط حق بیمه/وام قابل پرداختی ندارد', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '82b72870-5c78-4b01-9e25-07e8cdb33cbb', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_ANY_INSTALLMENT_MESSAGE', N'بیمه نامه عمر به شماره {0} هیچ قسط حق بیمه/وام قابل پرداختی ندارد', 'en-US', '0', GETDATE(), '82b72870-5c78-4b01-9e25-07e8cdb33cbb', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_AMOUNT_MESSAGE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'قسط قابل پرداخت بیمه نامه عمر به شماره {0} با مبلغ پرداختی این فاکتور مطابقت ندارد.', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'ee02424d-9fbc-43f8-906a-81c7d898b57f', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_AMOUNT_MESSAGE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_INSTALLMENT_AMOUNT_MESSAGE', N'قسط قابل پرداخت بیمه نامه عمر به شماره {0} با مبلغ پرداختی این فاکتور مطابقت ندارد.', 'fa-IR', '0', GETDATE(), 'ee02424d-9fbc-43f8-906a-81c7d898b57f', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_AMOUNT_MESSAGE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'قسط قابل پرداخت بیمه نامه عمر به شماره {0} با مبلغ پرداختی این فاکتور مطابقت ندارد.', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '97fb2519-211c-4c17-a95b-6afd8f6e472c', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_AMOUNT_MESSAGE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_INSTALLMENT_AMOUNT_MESSAGE', N'قسط قابل پرداخت بیمه نامه عمر به شماره {0} با مبلغ پرداختی این فاکتور مطابقت ندارد.', 'en-US', '0', GETDATE(), '97fb2519-211c-4c17-a95b-6afd8f6e472c', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_ORIGIN_SUBMIT_FAILED_MESSAGE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'قسط شماره {0} مربوط به بیمه نامه {1} قابل وصول نیست', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '33e6a3a1-5622-410d-a178-dfe01851c14b', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_ORIGIN_SUBMIT_FAILED_MESSAGE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_INSTALLMENT_ORIGIN_SUBMIT_FAILED_MESSAGE', N'قسط شماره {0} مربوط به بیمه نامه {1} قابل وصول نیست', 'fa-IR', '0', GETDATE(), '33e6a3a1-5622-410d-a178-dfe01851c14b', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_ORIGIN_SUBMIT_FAILED_MESSAGE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'قسط شماره {0} مربوط به بیمه نامه {1} قابل وصول نیست', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '18dea10b-6a59-4a34-aec0-2f2bb54e8fc5', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_ORIGIN_SUBMIT_FAILED_MESSAGE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_INSTALLMENT_ORIGIN_SUBMIT_FAILED_MESSAGE', N'قسط شماره {0} مربوط به بیمه نامه {1} قابل وصول نیست', 'en-US', '0', GETDATE(), '18dea10b-6a59-4a34-aec0-2f2bb54e8fc5', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'ORIGIN_SUBMITED' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'وصول شده', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '6b85de61-5c7d-4f21-8ada-b6bfd58bf700', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'ORIGIN_SUBMITED' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'ORIGIN_SUBMITED', N'وصول شده', 'fa-IR', '0', GETDATE(), '6b85de61-5c7d-4f21-8ada-b6bfd58bf700', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'ORIGIN_SUBMITED' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'وصول شده', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'e36a439c-5c14-45c9-ad98-7f3ec29de836', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'ORIGIN_SUBMITED' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'ORIGIN_SUBMITED', N'وصول شده', 'en-US', '0', GETDATE(), 'e36a439c-5c14-45c9-ad98-7f3ec29de836', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'IS_PAID' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'پرداخت شده', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '3741ca73-d17f-4f0e-b273-5e42bf18a2a7', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'IS_PAID' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'IS_PAID', N'پرداخت شده', 'fa-IR', '0', GETDATE(), '3741ca73-d17f-4f0e-b273-5e42bf18a2a7', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'IS_PAID' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'پرداخت شده', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'fb9451bc-43f4-477f-941a-ebb0f70b094d', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'IS_PAID' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'IS_PAID', N'پرداخت شده', 'en-US', '0', GETDATE(), 'fb9451bc-43f4-477f-941a-ebb0f70b094d', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'PAYMENT_SOURCE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'سرویس پرداخت کننده', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'e41fafe4-dcf1-43c1-ab59-81368f7bf70f', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'PAYMENT_SOURCE' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'PAYMENT_SOURCE', N'سرویس پرداخت کننده', 'fa-IR', '0', GETDATE(), 'e41fafe4-dcf1-43c1-ab59-81368f7bf70f', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'PAYMENT_SOURCE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'سرویس پرداخت کننده', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '5ac99b78-7b8e-4dea-b133-9c048ef53c4f', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'PAYMENT_SOURCE' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'PAYMENT_SOURCE', N'سرویس پرداخت کننده', 'en-US', '0', GETDATE(), '5ac99b78-7b8e-4dea-b133-9c048ef53c4f', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'SUBMIT_NEXT_INSTALLMENT' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'وصول به ازای پرداخت', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'a3995fa8-45a8-4432-89bb-aad171d8a0d2', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'SUBMIT_NEXT_INSTALLMENT' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'SUBMIT_NEXT_INSTALLMENT', N'وصول به ازای پرداخت', 'fa-IR', '0', GETDATE(), 'a3995fa8-45a8-4432-89bb-aad171d8a0d2', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'SUBMIT_NEXT_INSTALLMENT' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'وصول به ازای پرداخت', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'bb8816d2-abec-4052-8606-81355bdb8ffa', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'SUBMIT_NEXT_INSTALLMENT' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'SUBMIT_NEXT_INSTALLMENT', N'وصول به ازای پرداخت', 'en-US', '0', GETDATE(), 'bb8816d2-abec-4052-8606-81355bdb8ffa', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'INQUIRY_FAILED' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'استعلام ناموفق بود', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '8e5e2793-290f-4a66-9de2-d46d9c48bec2', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'INQUIRY_FAILED' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'INQUIRY_FAILED', N'استعلام ناموفق بود', 'fa-IR', '0', GETDATE(), '8e5e2793-290f-4a66-9de2-d46d9c48bec2', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'INQUIRY_FAILED' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'استعلام ناموفق بود', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'a5218211-f132-4047-9f8b-50bdf863a7ea', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'INQUIRY_FAILED' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'INQUIRY_FAILED', N'استعلام ناموفق بود', 'en-US', '0', GETDATE(), 'a5218211-f132-4047-9f8b-50bdf863a7ea', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'ORIGINALLY_SUBMITTED' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'از سمت کور وصول شده است', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '60f0b939-42d9-4da8-a559-5cee548560fc', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'ORIGINALLY_SUBMITTED' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'ORIGINALLY_SUBMITTED', N'از سمت کور وصول شده است', 'fa-IR', '0', GETDATE(), '60f0b939-42d9-4da8-a559-5cee548560fc', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'ORIGINALLY_SUBMITTED' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'از سمت کور وصول شده است', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '67af5209-c9c3-45a1-ab09-e674e379adcb', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'ORIGINALLY_SUBMITTED' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'ORIGINALLY_SUBMITTED', N'از سمت کور وصول شده است', 'en-US', '0', GETDATE(), '67af5209-c9c3-45a1-ab09-e674e379adcb', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'INQUIRY_LIFE_INSURANCE_INSTALLMENT' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'استعلام قسط عمر', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'c8750724-d408-4b97-ad4b-4f812d846704', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'INQUIRY_LIFE_INSURANCE_INSTALLMENT' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'INQUIRY_LIFE_INSURANCE_INSTALLMENT', N'استعلام قسط عمر', 'fa-IR', '0', GETDATE(), 'c8750724-d408-4b97-ad4b-4f812d846704', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'INQUIRY_LIFE_INSURANCE_INSTALLMENT' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'استعلام قسط عمر', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '3276f0ff-488c-4a76-b4d1-70577d070414', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'INQUIRY_LIFE_INSURANCE_INSTALLMENT' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'INQUIRY_LIFE_INSURANCE_INSTALLMENT', N'استعلام قسط عمر', 'en-US', '0', GETDATE(), '3276f0ff-488c-4a76-b4d1-70577d070414', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_INQUIRY_RESULT_WITH_FACTOR_ID' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'استعلام وصول قسط عمر موجود در فاکتور شماره', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '150bd43e-5a57-44bf-9761-d57c70511381', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_INQUIRY_RESULT_WITH_FACTOR_ID' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_INSTALLMENT_INQUIRY_RESULT_WITH_FACTOR_ID', N'استعلام وصول قسط عمر موجود در فاکتور شماره', 'fa-IR', '0', GETDATE(), '150bd43e-5a57-44bf-9761-d57c70511381', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_INQUIRY_RESULT_WITH_FACTOR_ID' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'استعلام وصول قسط عمر موجود در فاکتور شماره', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = 'd284507a-b7e9-4301-8bba-bfa6e4484bfa', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'LIFE_INSURANCE_INSTALLMENT_INQUIRY_RESULT_WITH_FACTOR_ID' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'LIFE_INSURANCE_INSTALLMENT_INQUIRY_RESULT_WITH_FACTOR_ID', N'استعلام وصول قسط عمر موجود در فاکتور شماره', 'en-US', '0', GETDATE(), 'd284507a-b7e9-4301-8bba-bfa6e4484bfa', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'INSTALLMENT_NOT_PAYABLE_TRY_NEXT_INSTALLMENT_INFO' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'قسط با اطلاعات زیر قابل پرداخت نیست. میتوانید تلاش کنید تا پرداخت را برای قسط بعدی کاربر انتقال دهید ', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '45e4c19b-dd4b-4081-a1c6-00fcb9521005', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'INSTALLMENT_NOT_PAYABLE_TRY_NEXT_INSTALLMENT_INFO' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'INSTALLMENT_NOT_PAYABLE_TRY_NEXT_INSTALLMENT_INFO', N'قسط با اطلاعات زیر قابل پرداخت نیست. میتوانید تلاش کنید تا پرداخت را برای قسط بعدی کاربر انتقال دهید ', 'fa-IR', '0', GETDATE(), '45e4c19b-dd4b-4081-a1c6-00fcb9521005', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'INSTALLMENT_NOT_PAYABLE_TRY_NEXT_INSTALLMENT_INFO' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'قسط با اطلاعات زیر قابل پرداخت نیست. میتوانید تلاش کنید تا پرداخت را برای قسط بعدی کاربر انتقال دهید ', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '0a737c11-6650-4925-9704-b9620efeb179', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'INSTALLMENT_NOT_PAYABLE_TRY_NEXT_INSTALLMENT_INFO' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'INSTALLMENT_NOT_PAYABLE_TRY_NEXT_INSTALLMENT_INFO', N'قسط با اطلاعات زیر قابل پرداخت نیست. میتوانید تلاش کنید تا پرداخت را برای قسط بعدی کاربر انتقال دهید ', 'en-US', '0', GETDATE(), '0a737c11-6650-4925-9704-b9620efeb179', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'RELOCATE_OR_SUBMIT_SUCCESSFULLY' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'وصول همین قسط یا اولین قسط قابل پرداخت موفق بود', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '048f1a4c-c210-46ea-bfa4-03c63a4711fd', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'RELOCATE_OR_SUBMIT_SUCCESSFULLY' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'RELOCATE_OR_SUBMIT_SUCCESSFULLY', N'وصول همین قسط یا اولین قسط قابل پرداخت موفق بود', 'fa-IR', '0', GETDATE(), '048f1a4c-c210-46ea-bfa4-03c63a4711fd', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'RELOCATE_OR_SUBMIT_SUCCESSFULLY' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'وصول همین قسط یا اولین قسط قابل پرداخت موفق بود', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '0bf5c305-b135-4a0d-af00-7007e8db0406', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'RELOCATE_OR_SUBMIT_SUCCESSFULLY' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'RELOCATE_OR_SUBMIT_SUCCESSFULLY', N'وصول همین قسط یا اولین قسط قابل پرداخت موفق بود', 'en-US', '0', GETDATE(), '0bf5c305-b135-4a0d-af00-7007e8db0406', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'RELOCATION_LIFE_INSURANCE_INSTALLMENT' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'وصول قسط بیمه عمر', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '0da3a299-4a91-43d8-8a11-1a159428c6d2', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'RELOCATION_LIFE_INSURANCE_INSTALLMENT' AND [Culture] = 'fa-IR' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'RELOCATION_LIFE_INSURANCE_INSTALLMENT', N'وصول قسط بیمه عمر', 'fa-IR', '0', GETDATE(), '0da3a299-4a91-43d8-8a11-1a159428c6d2', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
DECLARE @TenantCursor CURSOR 
DECLARE @TenantId INT 
DECLARE @TenantBusinessId UNIQUEIDENTIFIER 
 
SET @TenantCursor = CURSOR FOR 
SELECT Id, BusinessId FROM Tenants 
 
OPEN @TenantCursor 
FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
 
WHILE @@FETCH_STATUS = 0 
BEGIN 
    IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'RELOCATION_LIFE_INSURANCE_INSTALLMENT' AND [Culture] = 'en-US' AND [TenantId] = @TenantId) 
    BEGIN 
        UPDATE [dbo].[ParrotTranslations] SET [Value] = N'وصول قسط بیمه عمر', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '2a69007c-bba1-46fe-bdf1-cceb9e47d5ff', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'RELOCATION_LIFE_INSURANCE_INSTALLMENT' AND [Culture] = 'en-US' AND [TenantId] = @TenantId 
    END 
    ELSE 
    BEGIN 
        INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'RELOCATION_LIFE_INSURANCE_INSTALLMENT', N'وصول قسط بیمه عمر', 'en-US', '0', GETDATE(), '2a69007c-bba1-46fe-bdf1-cceb9e47d5ff', @TenantId, @TenantBusinessId) 
    END 
    FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId 
END 
 
CLOSE @TenantCursor 
DEALLOCATE @TenantCursor 
GO 
