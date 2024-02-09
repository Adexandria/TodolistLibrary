


namespace TasksLibrary.Models
{
    public class User : BaseClass , IUser
    {
        protected User()
        {

        }
        public User(string name,string email)
        {
            Name = name;
            Email = email;
            Notes = new List<INote>();
        }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual IRefreshToken RefreshToken { get; set; }
        public virtual IAccessToken AccessToken { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string Salt { get; set; }
        public virtual IList<INote> Notes { get; set; }
    }
}
