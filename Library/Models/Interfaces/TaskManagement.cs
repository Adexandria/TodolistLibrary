using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models.Interfaces
{
    public class TaskManagement
    {
        public INoteRepository NoteRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
    }
}
