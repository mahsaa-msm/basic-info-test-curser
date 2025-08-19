using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Infra.Data.Sql.Commands.Common.Conversions;
using Microsoft.EntityFrameworkCore;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Extensions;

public static class ConversionCollectionExtentions
{
    public static void AddConversions(this ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddIsActiveConversion();
        configurationBuilder.AddNameConversion();
    }

    #region IsActive
    public static void AddIsActiveConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<IsActive>().HaveConversion<IsActiveConversion>();
    #endregion
    #region Name
    public static void AddNameConversion(this ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.Properties<Name>().HaveConversion<NameConversion>();
    #endregion
}