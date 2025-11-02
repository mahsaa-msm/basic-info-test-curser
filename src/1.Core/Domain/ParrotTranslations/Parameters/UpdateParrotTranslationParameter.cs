namespace Master.Data.Core.Domain.ParrotTranslations.Parameters;

public sealed record UpdateParrotTranslationParameter(string Key,
                                                      string Value,
                                                      string? Culture);