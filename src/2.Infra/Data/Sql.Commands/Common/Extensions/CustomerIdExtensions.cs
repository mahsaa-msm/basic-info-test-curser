using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Zamin.Extensions.UsersManagement.Abstractions;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Extensions;
public static class CustomerIdExtensions
{
    //public static void SetCustomerIdValue(this ChangeTracker changeTracker, IUserInfoService userInfoService, SoftwareManagementOption softwareManagementOption)
    //{
    //    string? customerId = userInfoService.GetClaim(softwareManagementOption.CustomerIdClaimName)?.ToString();

    //    foreach (var entry in changeTracker.Entries<BaseCustomerEntity>())
    //    {
    //        if (entry.State == EntityState.Added && entry.Entity.CustomerId < 1)
    //        {
    //            entry.Entity.CustomerId = Convert.ToInt64(customerId);
    //        }
    //    }
    //}
}