using TasksLibrary.Utilities;
using TasksLibrary.Models;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Application.Queries.FetchAllNotes
{
    public class FetchAllNotesQueryHandler : QueryHandler<FetchAllNotesQuery, QueryContext<Note>, List<NoteDTO>>
    {
        public override async Task<ActionResult<List<NoteDTO>>> HandleAsync(FetchAllNotesQuery command)
        {
            var currentNotes = QueryContext.Entities.Where(s=>s.User.Id == command.UserId).OrderByDescending(s => s.Created).ToList();

            var notesDTO = currentNotes?.Select(s => new NoteDTO 
            { 
                Description = s.Description,
                Task = s.Task,
                Id = s.Id,
            }).ToList();

            return await Task.FromResult(SuccessfulOperation(notesDTO));
        }
    }
}
