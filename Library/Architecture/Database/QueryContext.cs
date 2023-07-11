using NHibernate;

namespace TasksLibrary.Architecture.Database
{
    public class QueryContext<T>
    {
        protected QueryContext() { }
        public QueryContext(ISession session)
        {
            Session = session;
        }

        public ISession Session { get; set; }

        public virtual IQueryable<T> Entities => Session.Query<T>();
    }
}
