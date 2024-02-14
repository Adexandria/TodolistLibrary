


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
        public virtual string PasswordHash { get; set; }
        public virtual string Salt { get; set; }
        public virtual RefreshTokenId RefreshTokenId { get; set; }
        public virtual AccessTokenId AccessTokenId { get; set; }
    }
}
