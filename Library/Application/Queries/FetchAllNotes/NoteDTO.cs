using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Application.Queries.FetchAllNotes
{
    public class NoteDTO
    {
        public Guid Id { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
    }
}
