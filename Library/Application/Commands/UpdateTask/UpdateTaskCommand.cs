using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Extensions;

namespace TasksLibrary.Application.Commands.UpdateTask
{
    public class UpdateTaskCommand : Command
    {
        public override ActionResult Validate()
        {
            return new RequestValidator()
              .IsText(Task, "Invalid task")
              .IsGuid(TaskId, "Invalid task id")
              .Result;
        }
        public Guid TaskId { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
    }
}
