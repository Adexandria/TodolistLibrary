using TasksLibrary.Models;

namespace TasksLibrary.DB.Mappings
{
    public class UserMap : ClassMapping<IUser>
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
            References(s => s.AccessToken).Cascade.All();
            References(s => s.RefreshToken).Cascade.All();
        }
    }
}
