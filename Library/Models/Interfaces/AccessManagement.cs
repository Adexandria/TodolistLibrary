using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models.Interfaces
{
    public class AccessManagement
    {
        public virtual IUserRepository  UserRepository { get;set;}
        public virtual IAuthToken AuthenTokenRepository { get; set; }
        public virtual IAccessTokenRepository AccessTokenRepository { get; set; }
        public virtual IRefreshTokenRepository RefreshTokenRepository { get; set; }
        
    }
}
