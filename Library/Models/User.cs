using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace TasksLibrary.Models
{
    public class User : BaseClass
    {
        protected User()
        {

        }
        public User(string name,string email)
        {
            Name = name;
            Email = email;
        }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual RefreshToken RefreshToken { get; set; }
        public virtual AccessToken AccessToken { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string Salt { get; set; }
        public virtual IList<Note> Notes { get; set; } = new List<Note>();
        
        
       
    }
}
