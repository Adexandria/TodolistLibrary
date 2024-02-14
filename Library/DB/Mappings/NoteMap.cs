
using TasksLibrary.Models;

namespace TasksLibrary.DB.Mappings
{
    public class NoteMap : ClassMapping<Note>
    {
        public NoteMap()
        {
            Cache.ReadWrite();
            Table("Notes");
            Map(s=>s.Task);
            Map(s=>s.Created);
            Map(s=>s.Modified);
            Map(s=>s.Description);
            Component(m => m.UserId, p =>
            {
                p.Map(s => s.Id, "User_id");
            });
        }
    }
}
