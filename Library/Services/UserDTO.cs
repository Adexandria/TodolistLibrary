using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Services
{
    public class UserDTO
    {
        public UserDTO(string userId, string email)
        {
            UserId = new Guid(userId);
            Email = email;
        }
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
