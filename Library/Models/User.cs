


namespace TasksLibrary.Models
{
    public class User : BaseClass , IUser
    {
        public User()
        {

        }
        public User(string firstName, string lastName,string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual IRefreshToken RefreshToken { get; set; }
        public virtual IAccessToken AccessToken { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string Salt { get; set; }
    }
}
