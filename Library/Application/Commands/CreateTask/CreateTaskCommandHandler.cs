using TasksLibrary.Architecture.Database;
using TasksLibrary.Utilities;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Architecture.Extensions;

namespace TasksLibrary.Application.Commands.CreateTask
{
    public class CreateTaskCommandHandler : CommandHandler<CreateTaskCommand, DbContext<TaskManagement>, Guid>
    {
        public override async Task<ActionResult<Guid>> HandleCommand(CreateTaskCommand command)
        {
            var currentUser = await Dbcontext.Context.UserRepository.GetExistingEntityById(command.UserId);

            if (currentUser == null)
                return FailedOperation("User doesn't exist",System.Net.HttpStatusCode.NotFound);

            var newNote = MapCommand.MapToEntity<CreateTaskCommand,INote>(command);

            if (!string.IsNullOrEmpty(command.Description))
                    newNote.Description = command.Description;

            await Dbcontext.Context.NoteRepository.Add(newNote);

            var commitStatus = await Dbcontext.CommitAsync();

            if (commitStatus.NotSuccessful)
                return FailedOperation("Failed to create task");

            return SuccessfulOperation(newNote.Id);
        }
    }
}
