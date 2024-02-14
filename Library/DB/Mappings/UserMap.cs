using TasksLibrary.Models;

namespace TasksLibrary.DB.Mappings
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Cache.ReadWrite();
            Table("Users");
            Map(s => s.Email);
            Map(s => s.FirstName);
            Map(s => s.LastName);
            Map(s => s.PasswordHash);
            Map(s => s.Salt);
            Component(m => m.RefreshTokenId, p =>
            {
                p.Map(s => s.Id, "RefreshToken_id");
            });
            Component(m => m.AccessTokenId, p =>
            {
                p.Map(s => s.Id, "AccessToken_id");
            });
        }
    }
}
