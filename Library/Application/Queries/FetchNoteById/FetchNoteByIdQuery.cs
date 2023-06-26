

using TasksLibrary.Application.Queries.FetchAllNotes;
using TasksLibrary.Utilities;

namespace TasksLibrary.Application.Queries.FetchNoteById
{
    public class FetchNoteByIdQuery : Query<NoteDTO>
    {
        public override ActionResult Validate()
        {
            return new RequestValidator()
                .IsGuid(TaskId, "Invalid task id")
                .IsGuid(UserId, "Invalid id")
                .Result;
        }
        public Guid TaskId { get; set; }    
        public Guid UserId { get; set; }
    }
}
