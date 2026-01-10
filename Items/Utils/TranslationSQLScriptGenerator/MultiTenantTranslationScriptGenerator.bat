@echo off
setlocal EnableDelayedExpansion

chcp 65001
set /p FileName="Insert a name for your sql file (Without .sql): "

echo USE [DIP_MasterData] >> "%FileName%.sql"
echo GO >> "%FileName%.sql"

:loop
CALL :proccessGetTranslation
set /p Continue="Do you have any record to add(yes/no): "
if /I "!Continue!" NEQ "no" goto loop
echo Scripts saved in '%FileName%.sql'.
endlocal
goto :eof

:proccessGetTranslation
set /p Key="Whats your 'Key': "
set /p Value="Whats '%Key%'s 'Value': "
set /p ApplyToAllTenants="Apply to all tenants? (yes/no): "

if /I "!ApplyToAllTenants!"=="yes" (
    echo -- Applying translation for all tenants >> "%FileName%.sql"
    
    for %%C in ("fa-IR" "en-US") do (
        set Culture=%%~C
        CALL :processForAllTenants
    )
) else (
    set /p TenantName="Enter tenant name: "
    echo -- Applying translation for tenant: !TenantName! >> "%FileName%.sql"
    
    for %%C in ("fa-IR" "en-US") do (
        set Culture=%%~C
        CALL :processForSingleTenant
    )
)

goto :eof

:processForAllTenants
for /f %%i in ('powershell -Command "[guid]::NewGuid().ToString()"') do set BusinessId=%%i
echo DECLARE @TenantCursor CURSOR >> "%FileName%.sql"
echo DECLARE @TenantId INT >> "%FileName%.sql"
echo DECLARE @TenantBusinessId UNIQUEIDENTIFIER >> "%FileName%.sql"
echo. >> "%FileName%.sql"
echo SET @TenantCursor = CURSOR FOR >> "%FileName%.sql"
echo SELECT Id, BusinessId FROM Tenants >> "%FileName%.sql"
echo. >> "%FileName%.sql"
echo OPEN @TenantCursor >> "%FileName%.sql"
echo FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId >> "%FileName%.sql"
echo. >> "%FileName%.sql"
echo WHILE @@FETCH_STATUS = 0 >> "%FileName%.sql"
echo BEGIN >> "%FileName%.sql"
echo     IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'!Key!' AND [Culture] = '!Culture!' AND [TenantId] = @TenantId) >> "%FileName%.sql"
echo     BEGIN >> "%FileName%.sql"
echo         UPDATE [dbo].[ParrotTranslations] SET [Value] = N'!Value!', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '!BusinessId!', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'!Key!' AND [Culture] = '!Culture!' AND [TenantId] = @TenantId >> "%FileName%.sql"
echo     END >> "%FileName%.sql"
echo     ELSE >> "%FileName%.sql"
echo     BEGIN >> "%FileName%.sql"
echo         INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'!Key!', N'!Value!', '!Culture!', '0', GETDATE(), '!BusinessId!', @TenantId, @TenantBusinessId) >> "%FileName%.sql"
echo     END >> "%FileName%.sql"
echo     FETCH NEXT FROM @TenantCursor INTO @TenantId, @TenantBusinessId >> "%FileName%.sql"
echo END >> "%FileName%.sql"
echo. >> "%FileName%.sql"
echo CLOSE @TenantCursor >> "%FileName%.sql"
echo DEALLOCATE @TenantCursor >> "%FileName%.sql"
echo GO >> "%FileName%.sql"
goto :eof

:processForSingleTenant
for /f %%i in ('powershell -Command "[guid]::NewGuid().ToString()"') do set BusinessId=%%i
echo DECLARE @TenantId INT >> "%FileName%.sql"
echo DECLARE @TenantBusinessId UNIQUEIDENTIFIER >> "%FileName%.sql"
echo. >> "%FileName%.sql"
echo SELECT @TenantId = Id, @TenantBusinessId = BusinessId FROM Tenants WHERE Name = '!TenantName!' >> "%FileName%.sql"
echo. >> "%FileName%.sql"
echo IF EXISTS (SELECT * FROM [dbo].[ParrotTranslations] WHERE [Key] = N'!Key!' AND [Culture] = '!Culture!' AND [TenantId] = @TenantId) >> "%FileName%.sql"
echo BEGIN >> "%FileName%.sql"
echo     UPDATE [dbo].[ParrotTranslations] SET [Value] = N'!Value!', [ModifiedByUserId] = '0', [ModifiedDateTime] = GETDATE(), [BusinessId] = '!BusinessId!', [TenantBusinessId] = @TenantBusinessId WHERE [Key] = N'!Key!' AND [Culture] = '!Culture!' AND [TenantId] = @TenantId >> "%FileName%.sql"
echo END >> "%FileName%.sql"
echo ELSE >> "%FileName%.sql"
echo BEGIN >> "%FileName%.sql"
echo     INSERT INTO [dbo].[ParrotTranslations] ([Key], [Value], [Culture], [CreatedByUserId], [CreatedDateTime], [BusinessId], [TenantId], [TenantBusinessId]) VALUES (N'!Key!', N'!Value!', '!Culture!', '0', GETDATE(), '!BusinessId!', @TenantId, @TenantBusinessId) >> "%FileName%.sql"
echo END >> "%FileName%.sql"
echo GO >> "%FileName%.sql"
goto :eof