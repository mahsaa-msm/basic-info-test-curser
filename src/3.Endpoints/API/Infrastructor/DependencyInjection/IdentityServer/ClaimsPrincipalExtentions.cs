using Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using System.Security.Claims;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer;

public static class ClaimsPrincipalExtentions
{
    public static bool HasSubClaim(this ClaimsPrincipal principal, string userIdentifierClaimType)
        => principal?.Claims.Any(c => c.Type.Equals(userIdentifierClaimType)) ?? false;

    public static ClaimsIdentity CreateClaimsIdentity(this ClaimsPrincipal principal, List<Claim> claims)
        => new(principal?.Claims?.ToList().GetNotExist([.. claims]),
               principal?.Identities.FirstOrDefault()?.AuthenticationType,
               principal?.Identities.FirstOrDefault()?.NameClaimType,
               principal?.Identities.FirstOrDefault()?.RoleClaimType);

    public static ClaimsPrincipal ClonePrincipalWithConvertedClaims(this ClaimsPrincipal principal, OAuthOption oAuthOption)
    {
        if (oAuthOption.UserClaimRules.Any(rule => string.IsNullOrWhiteSpace(rule.Source) || string.IsNullOrWhiteSpace(rule.Destination)))
            throw new ArgumentNullException("Source or Destination can not be null or white-space in UserClaimRule");

        if (principal is null) return null;

        ClaimsPrincipal clone = principal.Clone();

        List<Claim> claims = [];
        claims.AddRange(oAuthOption.UserClaimRulesProcesor([.. clone.Claims]));

        string authenticationType = clone.Identities.First().AuthenticationType;
        string nameType = clone.Identities.First().NameClaimType;
        string roleType = clone.Identities.First().RoleClaimType;

        ClaimsIdentity claimsIdentity = new(claims, authenticationType, nameType, roleType);

        return new(claimsIdentity);
    }

    private static List<Claim> UserClaimRulesProcesor(this OAuthOption oAuthOption, List<Claim> currentClaims)
    {
        List<Claim> newClaims = [];

        foreach (var item in currentClaims)
        {
            var userRuleClaim = oAuthOption.UserClaimRules.FirstOrDefault(claim => claim.Source.Equals(item.Type));
            if (userRuleClaim is not null)
            {
                var mappedClaim = new Claim(userRuleClaim.Destination,
                                            item.Value,
                                            item.ValueType,
                                            item.Issuer,
                                            item.OriginalIssuer,
                                            item.Subject);

                newClaims.Add(mappedClaim);

                if (userRuleClaim.RemoveSource is false)
                {
                    newClaims.Add(item);
                }
            }
            else
            {
                newClaims.Add(item);
            }
        }

        return newClaims;
    }

    private static List<Claim> GetNotExist(this List<Claim> current, List<Claim> target)
        => [.. target.Where(claim => !current.Any(currentClaim => currentClaim.Type.Equals(claim.Type) && currentClaim.Value.Equals(claim.Value)))];
}

