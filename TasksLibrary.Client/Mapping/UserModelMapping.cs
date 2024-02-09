using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Client.Model;
using TasksLibrary.Models;

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
            Map(s => s.Name);
            Map(s => s.PasswordHash);
            Map(s => s.Salt);
            Map(s => s.AuthenticationType);
            References(s => s.AccessToken).Cascade.All();
            References(s => s.RefreshToken).Cascade.All();
            HasMany(s => s.Notes).Table("Notes").Cascade.Delete();
        }
    }
}
