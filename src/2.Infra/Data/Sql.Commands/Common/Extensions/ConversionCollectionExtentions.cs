using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Infra.Data.Sql.Commands.Common.Conversions;
using Microsoft.EntityFrameworkCore;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Extensions;

public static class ConversionCollectionExtentions
{
    public static void AddConversions(this ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddCodeConversion();
        configurationBuilder.AddCoreIdConversion();
        configurationBuilder.AddIsActiveConversion();
        configurationBuilder.AddIsDeletedConversion();
        configurationBuilder.AddDIPTitleConversion();
        configurationBuilder.AddNullableTitleConversion();
        configurationBuilder.AddPriorityConversion();
        configurationBuilder.AddNameConversion();
        configurationBuilder.AddPercentageConversion();
    }

    #region Code
    public static void AddCodeConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<Code>().HaveConversion<CodeConversion>();
    #endregion
    #region CoreId
    public static void AddCoreIdConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<CoreId>().HaveConversion<CoreIdConversion>();
    #endregion
    #region IsActive
    public static void AddIsActiveConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<IsActive>().HaveConversion<IsActiveConversion>();
    #endregion
    #region IsDeleted
    public static void AddIsDeletedConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<IsDeleted>().HaveConversion<IsDeletedConversion>();
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
    #region Percentage
    public static void AddPercentageConversion(this ModelConfigurationBuilder configurationBuilder)
    => configurationBuilder.Properties<Percentage>().HaveConversion<PercentageConversion>();
    #endregion
}