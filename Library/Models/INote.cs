using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models
{
    public interface INote: IEntity
    {
        abstract string Task { get; set; }
        abstract string Description { get; set; }
        abstract DateTime Created { get; set; }
        abstract DateTime Modified { get; set; }
        abstract UserId UserId { get; set; }

    }
}
