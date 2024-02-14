using FluentNHibernate.Mapping;
using TasksLibrary.Client.Model;

namespace TasksLibrary.Client.Mapping
{
    public class UserModelMapping : ClassMap<UserModel>
    {
        public UserModelMapping()
        {
            Cache.ReadWrite();
            Table("Users");
            Id(s=>s.Id);
            Map(s => s.Email);
            Map(s => s.FirstName);
            Map(s => s.LastName);
            Map(s => s.PasswordHash);
            Map(s => s.Salt);
            Map(s => s.AuthenticationType);
            Map(s=>s.UserName).Nullable();
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
