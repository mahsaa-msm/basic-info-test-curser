use [DIP_SoftwareManagement_DB]
Go


if not exists(select 1 from [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]
	where [Key] = 'ISSUANCE_SCHEME_ID' and Culture = 'en-US')
begin
	insert into [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]([Key], Value, Culture, CreatedByUserId, CreatedDateTime, ModifiedByUserId, ModifiedDateTime, BusinessId)
	values( 'ISSUANCE_SCHEME_ID', N'طرح صدور', 'en-US', 1, getdate(), null, getdate(),newid());
end



if not exists(select 1 from [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]
	where [Key] = 'ISSUANCE_SCHEME_ID' and Culture = 'en-US')
begin
	insert into [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]([Key], Value, Culture, CreatedByUserId, CreatedDateTime, ModifiedByUserId, ModifiedDateTime, BusinessId)
	values( 'ISSUANCE_SCHEME_ID', N'شناسه طرح صدور', 'en-US', 1, getdate(), null, getdate(),newid());
end




if not exists(select 1 from [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]
	where [Key] = 'ADJUSTMENT_TYPE_SURCHARGE' and Culture = 'en-US')
begin
	insert into [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]([Key], Value, Culture, CreatedByUserId, CreatedDateTime, ModifiedByUserId, ModifiedDateTime, BusinessId)
	values( 'ADJUSTMENT_TYPE_SURCHARGE', N'اضافه', 'en-US', 1, getdate(), null, getdate(),newid());
end


if not exists(select 1 from [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]
	where [Key] = 'ADJUSTMENT_TYPE_DISCOUNT' and Culture = 'en-US')
begin
	insert into [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]([Key], Value, Culture, CreatedByUserId, CreatedDateTime, ModifiedByUserId, ModifiedDateTime, BusinessId)
	values( 'ADJUSTMENT_TYPE_DISCOUNT', N'تخفیف', 'en-US', 1, getdate(), null, getdate(),newid());
end



if not exists(select 1 from [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]
	where [Key] = 'ADJUSTMENT_TYPE' and Culture = 'en-US')
begin
	insert into [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]([Key], Value, Culture, CreatedByUserId, CreatedDateTime, ModifiedByUserId, ModifiedDateTime, BusinessId)
	values( 'ADJUSTMENT_TYPE', N'نوع تخفیف اضافه', 'en-US', 1, getdate(), null, getdate(),newid());
end


if not exists(select 1 from [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]
	where [Key] = 'ADJUSTMENT_PERCENT' and Culture = 'en-US')
begin
	insert into [DIP_SoftwareManagement_DB].[dbo].[ParrotTranslations]([Key], Value, Culture, CreatedByUserId, CreatedDateTime, ModifiedByUserId, ModifiedDateTime, BusinessId)
	values( 'ADJUSTMENT_PERCENT', N'درصد تخفیف اضافه', 'en-US', 1, getdate(), null, getdate(),newid());
end