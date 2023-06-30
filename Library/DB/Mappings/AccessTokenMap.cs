using TasksLibrary.Models;

namespace TasksLibrary.DB.Mappings
{
    public class AccessTokenMap : ClassMapping<AccessToken>
    {
        public AccessTokenMap()
        {
            Cache.ReadWrite();
            Table("AccessTokens");
            Map(s => s.Token);
            Component(m => m.UserId, p =>
            {
                p.Map(s => s.Id,"User_id");
            });
        }
    }
}
