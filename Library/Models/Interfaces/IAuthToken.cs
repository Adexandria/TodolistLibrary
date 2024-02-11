using System.Security.Claims;
using TasksLibrary.Services;

namespace TasksLibrary.Models.Interfaces
{
    public interface IAuthToken
    {
        string GenerateAccessToken(Dictionary<string, object> claims , int timeInMinutes = 30);
        string GenerateRefreshToken(int tokenSize = 32);
        ClaimsPrincipal VerifyToken(string token);
        string TokenEncryptionKey { get; }
    }
}
