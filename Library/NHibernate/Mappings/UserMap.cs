using TasksLibrary.Models;

namespace TasksLibrary.NHibernate.Mappings
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("Users");
            Map(s => s.Email);
            Map(s => s.Name);
            Map(s => s.PasswordHash);
            Map(s => s.Salt);
            References(s => s.AccessToken).Cascade.All();
            References(s => s.RefreshToken).Cascade.All();
            HasMany(s=>s.Notes).Cascade.Delete();
        }
    }
}
