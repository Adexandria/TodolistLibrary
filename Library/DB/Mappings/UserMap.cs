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
            Map(s => s.Name);
            Map(s => s.PasswordHash);
            Map(s => s.Salt);
            References(s => s.AccessToken).Not.LazyLoad().Cascade.All();
            References(s => s.RefreshToken).Not.LazyLoad().Cascade.All();
            HasMany(s=>s.Notes).Not.LazyLoad().Cascade.Delete();
        }
    }
}
