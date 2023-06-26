using TasksLibrary.Utilities;

namespace TasksLibrary.Application.Commands.DeleteTask
{
    public class DeleteTaskCommand : Command
    {
        public override ActionResult Validate()
        {
            return new RequestValidator()
                 .IsGuid(TaskId, "Invalid task id").Result;
        }

        public Guid TaskId { get; set; }
    }
}
