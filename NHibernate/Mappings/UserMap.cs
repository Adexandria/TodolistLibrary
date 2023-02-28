using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;

namespace TasksLibrary.NHibernate.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(s => s.Email);
            Map(s => s.Name);
            Map(s => s.PasswordHash);
            References(s => s.AccessToken).Cascade.All();
            References(s => s.RefreshToken).Cascade.All();
        }
    }
}
