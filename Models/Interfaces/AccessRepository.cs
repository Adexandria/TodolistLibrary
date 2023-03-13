using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models.Interfaces
{
    public class AccessRepository
    {
        public IUserRepository  UserRepository { get;set;}
        public IAuthToken AuthToken { get; set; }
        
        
    }
}
