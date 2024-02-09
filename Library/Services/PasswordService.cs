

namespace TasksLibrary.Services
{
    public abstract class PasswordService : IPasswordManager
    {
        public abstract string HashPassword(string password, out string salt);
        public abstract bool VerifyPassword(string password, string currentPassword, string salt);
    }
}
