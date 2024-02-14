

namespace TasksLibrary.Models
{
    public interface IUser : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string PasswordHash { get; set; }
        string Salt { get; set; }
        RefreshTokenId RefreshTokenId { get; set; }
        AccessTokenId AccessTokenId { get; set; }
    }
}
