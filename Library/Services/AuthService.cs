
using System.Security.Claims;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services
{
    public abstract class AuthService : IAuthToken
    {
        public abstract string GenerateAccessToken(Dictionary<string, object> claims, int timeInMinutes);
        public abstract string GenerateRefreshToken(int tokenSize);
        public abstract ClaimsPrincipal VerifyToken(string token);
        public abstract string TokenEncryptionKey { get; }
    }
}
