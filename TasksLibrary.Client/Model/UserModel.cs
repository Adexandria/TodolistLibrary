using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;

namespace TasksLibrary.Client.Model
{
    public class UserModel : IUser
    {
        public UserModel()
        {
        }
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual RefreshTokenId RefreshTokenId { get; set ; }
        public virtual AccessTokenId AccessTokenId { get; set ; }
        public virtual string PasswordHash { get; set; }
        public virtual string Salt { get; set; }
        public virtual string AuthenticationType { get; set; } = "Default";
        public virtual string UserName { get; set; }
    }
}
