using TasksLibrary.Application.Queries.FetchAllNotes;
using TasksLibrary.Utilities;
using TasksLibrary.Models;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Application.Queries.FetchNoteById
{
    public class FetchNoteByIdQueryHandler : QueryHandler<FetchNoteByIdQuery, QueryContext<Note>, NoteDTO>
    {
        public override async Task<ActionResult<NoteDTO>> HandleAsync(FetchNoteByIdQuery command)
        {
            var currentTask = QueryContext.Entities.Where(s => s.Id == command.TaskId && s.User.Id == command.UserId).FirstOrDefault();

            if (currentTask == null)
                return await Task.FromResult(FailedOperation("Note doesn't exist",System.Net.HttpStatusCode.NotFound));

            var noteDto = new NoteDTO
            {
                Description = currentTask.Description,
                Id = command.TaskId,
                Task = currentTask.Task
            };

            return await Task.FromResult(SuccessfulOperation(noteDto));
        }
    }
}
