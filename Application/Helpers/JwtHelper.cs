using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Models.Jwt;
using Core.Entity.Concrete;
using Microsoft.IdentityModel.Tokens;
using Claim = System.Security.Claims.Claim;

namespace Application.Helpers;

public class JwtHelper
{
    public static string GenerateJwtToken(PayloadRequirements payload, List<Role> roles)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Base64UrlEncoder.DecodeBytes(Accessor.AppConfiguration!["JwtSettings:Secret"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = SetClaims(payload, roles),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity SetClaims( PayloadRequirements payload, List<Role> roles)
    {
        List<System.Security.Claims.Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, payload.Id.ToString()),
            new Claim(ClaimTypes.Name, payload.Username),
            new Claim(ClaimTypes.Email, payload.Email)
        };

        if (roles.Count != 0)
            claims.AddRoles(roles.Select(x => x.Name));

        return new ClaimsIdentity(claims);
    }
}