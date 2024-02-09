
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services
{
    public abstract class AuthService : IAuthToken
    {
        public abstract string GenerateAccessToken(Guid userId, string email);
        public abstract string GenerateRefreshToken();
        public abstract UserDTO VerifyToken(string token);
    }
}
