using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Shared.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId() => GetClaim(ClaimTypes.NameIdentifier);
    public int UserId => int.Parse(GetClaim("nameid"));
    public string GetClaim(string key) => _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value ?? "1";
}