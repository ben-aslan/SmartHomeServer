using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Core.Extensions;

public static class ClaimExtensions
{
    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
    }

    public static void AddName(this ICollection<Claim> claims, string name)
    {
        claims.Add(new Claim(ClaimTypes.Name, name));
    }

    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
    }

    public static void AddRoles(this ICollection<Claim> claims, string[] roles)
    {
        roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
    }

    public static string? GetNameIdentifier(this IEnumerable<Claim> claims)
    {
        return claims?.SingleOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
    }

    public static int GetNameIdentifierAsInt32(this IEnumerable<Claim> claims)
    {
        return Convert.ToInt32(claims?.SingleOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value);
    }
}
