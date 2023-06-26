using TasksLibrary.Utilities;

namespace TasksLibrary.Application.Queries.FetchAllNotes
{
    public class FetchAllNotesQuery : Query<List<NoteDTO>>
    {
        public override ActionResult Validate()
        {
            return new RequestValidator().
                IsGuid(UserId, "Invalid user id")
                .Result;
        }
        public Guid UserId { get; set; }
    }
}
