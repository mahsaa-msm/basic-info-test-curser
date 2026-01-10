USE [DIP_MasterData]
GO

-- Declare cursor for all tenants
DECLARE @TenantId INT
DECLARE @TenantBusinessId UNIQUEIDENTIFIER

DECLARE tenant_cursor CURSOR FOR
SELECT Id, BusinessId FROM Tenants

OPEN tenant_cursor
FETCH NEXT FROM tenant_cursor INTO @TenantId, @TenantBusinessId

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Insert new translations from source database for en-US culture only
    -- Only insert if the key doesn't already exist in the target database for this tenant
    INSERT INTO [dbo].[ParrotTranslations] (
        [Key],
        [Value],
        [Culture],
        [CreatedByUserId],
        [CreatedDateTime],
        [BusinessId],
        [TenantId],
        [TenantBusinessId]
    )
    SELECT 
        src.[Key],
        src.[Value],
        src.[Culture],
        '0', -- CreatedByUserId
        GETDATE(), -- CreatedDateTime
        NEWID(), -- Generate new BusinessId
        @TenantId, -- Current tenant from cursor
        @TenantBusinessId -- Tenant BusinessId from cursor
    FROM [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations] src
    WHERE src.Culture = 'en-US'
        AND NOT EXISTS (
            SELECT 1 
            FROM [dbo].[ParrotTranslations] dest 
            WHERE dest.[Key] = src.[Key] 
                AND dest.[Culture] = src.[Culture] 
                AND dest.[TenantId] = @TenantId
        )

    FETCH NEXT FROM tenant_cursor INTO @TenantId, @TenantBusinessId
END

CLOSE tenant_cursor
DEALLOCATE tenant_cursor
GO