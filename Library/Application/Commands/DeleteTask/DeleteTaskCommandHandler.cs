using TasksLibrary.Utilities;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Application.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : CommandHandler<DeleteTaskCommand, DbContext<TaskManagement>>
    {
        public override async Task<ActionResult> HandleCommand(DeleteTaskCommand command)
        {
           var currentTask = await Dbcontext.Context.NoteRepository.GetExistingEntityById(command.TaskId);

            if (currentTask == null)
                return FailedOperation("Note doesn't exist",System.Net.HttpStatusCode.BadRequest);

            await Dbcontext.Context.NoteRepository.Delete(currentTask);

            var commitStatus = await Dbcontext.CommitAsync();

            if (commitStatus.NotSuccessful)
                return FailedOperation("Failed to delete note");

            return SuccessfulOperation();
        }

    }
}
