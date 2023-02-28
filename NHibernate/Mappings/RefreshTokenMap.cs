using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;

namespace TasksLibrary.NHibernate.Mappings
{
    public class RefreshTokenMap : ClassMap<RefreshToken>
    {
        public RefreshTokenMap()
        {
            Table("RefreshTokens");
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(s => s.Expires);
            Map(s => s.IsRevoked);
            Map(s => s.Token);
            Component(m => m.UserId, p =>
            {
                p.Map(s => s.Id);
            });
        }
    }
}
