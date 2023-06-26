using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;

namespace TasksLibrary.DB.Mappings
{
    public class AccessTokenMap : ClassMapping<AccessToken>
    {
        public AccessTokenMap()
        {
            Table("AccessTokens");
            Map(s => s.Token);
            Component(m => m.UserId, p =>
            {
                p.Map(s => s.Id,"User_id");
            });
        }
    }
}
