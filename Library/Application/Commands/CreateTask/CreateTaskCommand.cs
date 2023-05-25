using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Extensions;

namespace TasksLibrary.Application.Commands.CreateTask
{
    public class CreateTaskCommand : Command<Guid>
    {
        public override ActionResult Validate()
        {
            return new RequestValidator()
                .IsText(Task, "Invalid task")
                .IsGuid(UserId, "Invalid user id")
                .Result;
        }

        public string Task { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

    }
}
