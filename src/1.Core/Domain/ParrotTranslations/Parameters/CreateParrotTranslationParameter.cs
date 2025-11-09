namespace Master.Data.Core.Domain.ParrotTranslations.Parameters;

public sealed record CreateParrotTranslationParameter(string Key,
                                                      string Value,
                                                      string? Culture);