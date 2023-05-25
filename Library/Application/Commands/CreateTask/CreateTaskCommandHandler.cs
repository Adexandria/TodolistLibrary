using TasksLibrary.Extensions;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services.Architecture.Database;

namespace TasksLibrary.Application.Commands.CreateTask
{
    public class CreateTaskCommandHandler : CommandHandler<CreateTaskCommand, DbContext<TaskManagement>, Guid>
    {
        public override async Task<ActionResult<Guid>> HandleCommand(CreateTaskCommand command)
        {
            var currentUser = await Dbcontext.Context.UserRepository.GetExistingEntityById(command.UserId);

            if (currentUser == null)
                return FailedOperation("User doesn't exist");

            var newNote = new Note(command.Task);

            if(command.Description != null) 
                newNote.SetDescription(command.Description);

            newNote.SetUser(currentUser);

            await Dbcontext.Context.NoteRepository.Add(newNote);

            var commitStatus = await Dbcontext.CommitAsync();

            if (commitStatus.NotSuccessful)
                return FailedOperation("Failed to create task");

            return SuccessfulOperation(newNote.Id);
        }
    }
}
