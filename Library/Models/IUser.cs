

namespace TasksLibrary.Models
{
    public interface IUser : IEntity
    {
        abstract string FirstName { get; set; }
        abstract string LastName { get; set; }
        abstract string Email { get; set; }
        abstract IRefreshToken RefreshToken { get; set; }
        abstract IAccessToken AccessToken { get; set; }
        abstract string PasswordHash { get; set; }
        abstract string Salt { get; set; }
    }
}
