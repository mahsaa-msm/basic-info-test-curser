using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.ParrotTranslations.Parameters;

namespace Vehicle.Insurance.Core.Domain.ParrotTranslations.Entities;

public sealed class ParrotTranslation : BaseTenantEntity
{
    #region Properties
    public string Key { get; private set; }
    public string Value { get; private set; }
    public string? Culture { get; private set; }
    #endregion

    #region Constructors
    private ParrotTranslation() { }

    private ParrotTranslation(CreateParrotTranslationParameter parameter)
    {
        Key = parameter.Key;
        Value = parameter.Value;
        Culture = parameter.Culture;
    }
    #endregion

    #region Commands
    public static ParrotTranslation Create(CreateParrotTranslationParameter parameter)
        => new(parameter);

    public void Update(UpdateParrotTranslationParameter parameter)
    {
        Key = parameter.Key;
        Value = parameter.Value;
        Culture = parameter.Culture;
    }
    #endregion
}

