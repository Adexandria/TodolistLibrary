
using TasksLibrary.Models;

namespace TasksLibrary.DB.Mappings
{
    public class NoteMap : ClassMapping<Note>
    {
        public NoteMap()
        {
            Table("Notes");
            Map(s=>s.Task);
            Map(s=>s.Created);
            Map(s=>s.Modified);
            Map(s=>s.Description);
            References(s=>s.User);
        }
    }
}
