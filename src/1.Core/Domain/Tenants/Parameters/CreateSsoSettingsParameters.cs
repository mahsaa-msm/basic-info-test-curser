namespace Master.Data.Core.Domain.Tenants.Parameters;
public sealed record CreateSsoSettingsParameters(string SsoBasePath,
                                                 string UserName,
                                                 string Password,
                                                 string OauthType);