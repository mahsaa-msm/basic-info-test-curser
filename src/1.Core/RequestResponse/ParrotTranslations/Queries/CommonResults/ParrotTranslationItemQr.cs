namespace Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;

public sealed class ParrotTranslationItemQr
{
    public long Id { get; set; }
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string? Culture { get; set; }
}

