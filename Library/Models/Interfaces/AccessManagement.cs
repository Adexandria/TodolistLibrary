using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models.Interfaces
{
    public class AccessManagement
    {
        public IUserRepository  UserRepository { get;set;}
        public IAuthToken AuthenTokenRepository { get; set; }
        public IAccessTokenRepository AccessTokenRepository { get; set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; set; }
        
    }
}
