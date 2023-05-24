using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.NHibernate;

namespace TasksLibrary.Services
{
    public class AccessTokenRepository : Repository<AccessToken>, IAccessTokenRepository
    {
        public AccessTokenRepository(ISession session) : base(session)
        {
        }
    }
}
