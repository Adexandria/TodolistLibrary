using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;

namespace TasksLibrary.NHibernate.Mappings
{
    public class AccessTokenMap : ClassMap<AccessToken>
    {
        public AccessTokenMap()
        {
            Table("AccessTokens");
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(s => s.Token);
            Component(m => m.UserId, p =>
            {
                p.Map(s => s.Id);
            });
        }
    }
}
