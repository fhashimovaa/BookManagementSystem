namespace Shared.Services
{
    public interface IAuthService
    {
        string GetUserId();

        int UserId { get; }
        string GetClaim(string key);
    }
}