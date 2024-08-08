using System.Security.Claims;

namespace Application.Models.Jwt;

public static class ClaimExtensions
{
    public static void AddRoles(this ICollection<Claim> claims, IEnumerable<string> roles) =>
        roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
}