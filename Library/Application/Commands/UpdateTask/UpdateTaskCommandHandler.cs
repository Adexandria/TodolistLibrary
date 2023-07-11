using TasksLibrary.Utilities;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Application.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : CommandHandler<UpdateTaskCommand, DbContext<TaskManagement>>
    {
        public override async Task<ActionResult> HandleCommand(UpdateTaskCommand command)
        {
            var currentTask = await Dbcontext.Context.NoteRepository.GetExistingEntityById(command.TaskId);
            if (currentTask == null)
                return FailedOperation("Note doesn't exist",System.Net.HttpStatusCode.NotFound);

            currentTask.Task = command.Task;
            currentTask.Modified = DateTime.UtcNow;

            if(!string.IsNullOrEmpty(command.Description)) 
                currentTask.Description = command.Description;

            await Dbcontext.Context.NoteRepository.Update(currentTask);

            var commitStatus = await Dbcontext.CommitAsync();

            if (commitStatus.NotSuccessful)
                return FailedOperation("Failed to update note");

            return SuccessfulOperation();
        }
    }
}
