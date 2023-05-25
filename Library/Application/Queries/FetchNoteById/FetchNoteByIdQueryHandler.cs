using NHibernate.Linq;
using TasksLibrary.Application.Queries.FetchAllNotes;
using TasksLibrary.Extensions;
using TasksLibrary.Models;
using TasksLibrary.Services.Architecture.Database;

namespace TasksLibrary.Application.Queries.FetchNoteById
{
    public class FetchNoteByIdQueryHandler : QueryHandler<FetchNoteByIdQuery, QueryContext<Note>, NoteDTO>
    {
        public override async Task<ActionResult<NoteDTO>> HandleAsync(FetchNoteByIdQuery command)
        {
            var currentTask = await QueryContext.Entities.Where(s => s.Id.Equals(command.TaskId) && s.User.Id == command.UserId).FirstOrDefaultAsync();

            if (currentTask == null)
                return FailedOperation("Note doesn't exist",System.Net.HttpStatusCode.BadRequest);

            var noteDto = new NoteDTO
            {
                Description = currentTask.Description,
                Id = command.TaskId,
                Task = currentTask.Task
            };

            return SuccessfulOperation(noteDto);
        }
    }
}
