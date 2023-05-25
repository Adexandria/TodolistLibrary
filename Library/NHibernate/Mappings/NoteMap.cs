using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;

namespace TasksLibrary.NHibernate.Mappings
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
