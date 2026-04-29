namespace Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;

public sealed class ParrotTranslationQr
{
    public List<ParrotTranslationItemQr> ParrotTranslationItemQrs { get; set; } = new();
    public DateTime ExpireDateUtc { get; set; }
}
