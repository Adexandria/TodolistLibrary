using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.NHibernate;

namespace TasksLibrary.Services
{
    public class Repository<T>
    {
        public ISession Session;
        public Repository(SessionFactory session)
        {
            Session = session.GetSession();
        }

        public void Add(T entity)
        {
            Session.Save(entity);
        }

        public void Update(T entity)
        {
            Session.Update(entity);
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return Session.Query<T>();
        }
    }
}
