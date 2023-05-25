using NHibernate;

namespace TasksLibrary.Services.Architecture.Database
{
    public class QueryContext<T>
    {
        public QueryContext(ISession session)
        {
            Session = session;
        }

        public ISession Session { get; set; }

        public IQueryable<T> Entities => Session.Query<T>();
    }
}
