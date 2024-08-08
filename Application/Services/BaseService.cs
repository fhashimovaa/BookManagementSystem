using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Application.Services;

public abstract class BaseService
{
    protected readonly IHttpContextAccessor HttpContextAccessor;
    protected readonly IAuthService _authService;
    protected int UserId => _authService.UserId;

    protected BaseService(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
        _authService = HttpContextAccessor.HttpContext?.RequestServices?.GetService<IAuthService>() ??
                       throw new ArgumentException("Auth service can't be null");
    }
}