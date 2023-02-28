using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace TasksLibrary.Models
{
    public class User : BaseClass
    {
        public User(string name,string email)
        {
            Name = name;
            Email = email;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public AccessToken AccessToken { get; set; }
        public string PasswordHash { get; set; }
        
        
        public string HashPassword(string password, out string salt)
        {
            salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hash;
        }
        public bool VerifyPassword(string password, string salt)
        {
            var hash = HashPassword(password, out salt);
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
