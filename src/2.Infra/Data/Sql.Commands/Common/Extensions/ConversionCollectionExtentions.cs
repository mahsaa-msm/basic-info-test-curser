using Microsoft.EntityFrameworkCore;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Conversions;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Extensions;

public static class ConversionCollectionExtentions
{
    public static void AddConversions(this ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddCodeConversion();
        configurationBuilder.AddColorHashConversion();
        configurationBuilder.AddCoreIdConversion();
        configurationBuilder.AddIsActiveConversion();
        configurationBuilder.AddDIPTitleConversion();
        configurationBuilder.AddNullableTitleConversion();
        configurationBuilder.AddPriorityConversion();
        configurationBuilder.AddNameConversion();
        configurationBuilder.AddPercentageConversion();
        configurationBuilder.AddNullablePercentageConversion();
        configurationBuilder.AddNullableCoreIdConversion();
    }

    #region Code
    public static void AddCodeConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<Code>().HaveConversion<CodeConversion>();
    #endregion
    #region ColorHash
    public static void AddColorHashConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<ColorHash>().HaveConversion<ColorHashConversion>();
    #endregion
    #region CoreId
    public static void AddCoreIdConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<CoreId>().HaveConversion<CoreIdConversion>();
    #endregion
    #region IsActive
    public static void AddIsActiveConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<IsActive>().HaveConversion<IsActiveConversion>();
    #endregion
    #region DIPTitle
    public static void AddDIPTitleConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<DIPTitle>().HaveConversion<DIPTitleConversion>();
    #endregion
    #region NullableTitle
    public static void AddNullableTitleConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<NullableTitle>().HaveConversion<NullableTitleConversion>();
    #endregion
    #region Priority
    public static void AddPriorityConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<Priority>().HaveConversion<PriorityConversion>();
    #endregion
    #region Name
    public static void AddNameConversion(this ModelConfigurationBuilder configurationBuilder)
    => configurationBuilder.Properties<Name>().HaveConversion<NameConversion>();
    #endregion  
    #region NullableCoreId
    public static void AddNullableCoreIdConversion(this ModelConfigurationBuilder configurationBuilder)
    => configurationBuilder.Properties<NullableCoreId>().HaveConversion<NullableCoreIdConversion>();
    #endregion
    #region Percentage
    public static void AddPercentageConversion(this ModelConfigurationBuilder configurationBuilder)
    => configurationBuilder.Properties<Percentage>().HaveConversion<PercentageConversion>();

    public static void AddNullablePercentageConversion(this ModelConfigurationBuilder configurationBuilder)
    => configurationBuilder.Properties<NullablePercentage>().HaveConversion<NullablePercentageConversion>();
    #endregion
}
