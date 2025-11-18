USE [DIP_MasterData]
GO

-- Alternative approach using CROSS JOIN for better performance with many tenants
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
    t.Id, -- TenantId
    t.BusinessId -- TenantBusinessId
FROM [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations] src
CROSS JOIN Tenants t
WHERE src.Culture = 'en-US'
    AND NOT EXISTS (
        SELECT 1 
        FROM [dbo].[ParrotTranslations] dest 
        WHERE dest.[Key] = src.[Key] 
            AND dest.[Culture] = src.[Culture] 
            AND dest.[TenantId] = t.Id
    )
GO