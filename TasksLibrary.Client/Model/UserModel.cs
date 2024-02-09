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
                Notes = new List<INote>();
        }
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual IRefreshToken RefreshToken { get; set ; }
        public virtual IAccessToken AccessToken { get; set ; }
        public virtual string PasswordHash { get; set; }
        public virtual string Salt { get; set; }
        public virtual string AuthenticationType { get; set; } = "Default";
        public virtual IList<INote> Notes { get; set; }
    }
}
