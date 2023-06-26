using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;

namespace TasksLibrary.DB.Mappings
{
    public class RefreshTokenMap : ClassMapping<RefreshToken>
    {
        public RefreshTokenMap()
        {
            Table("RefreshTokens");
            Map(s => s.Expires);
            Map(s => s.IsRevoked);
            Map(s => s.Token);
            Component(m => m.UserId, p =>
            {
                p.Map(s => s.Id,"User_id");
            });
        }
    }
}
