using TasksLibrary.Services;

namespace TasksLibrary.Models.Interfaces
{
    public interface IAuthToken
    {
        string GenerateAccessToken(Guid userId,string email);
        string GenerateRefreshToken();
        UserDTO VerifyToken(string token);
        
        
    }
}
