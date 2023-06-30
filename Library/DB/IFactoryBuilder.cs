using NHibernate;


namespace TasksLibrary.DB
{
    public interface IFactoryBuilder
    {
        public ISession Session { get; }
    }
}
