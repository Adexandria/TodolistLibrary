using NHibernate;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(ISession session) : base(session)
        {
        }
    }
}
