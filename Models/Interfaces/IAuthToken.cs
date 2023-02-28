using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models.Interfaces
{
    public interface IAuthToken
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        string VerifyToken(string token);
        
    }
}
