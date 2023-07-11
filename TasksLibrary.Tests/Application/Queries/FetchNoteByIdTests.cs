using TasksLibrary.Application.Queries.FetchAllNotes;
using TasksLibrary.Application.Queries.FetchNoteById;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Tests.Utilities;

namespace TasksLibrary.Tests.Application.Queries
{
    [TestFixture]
    public class FetchNoteByIdTests : QueryHandlerTest<FetchNoteByIdQuery, FetchNoteByIdQueryHandler, QueryContext<Note>, Note, NoteDTO>
    {
        [Test]
        public async Task ShouldFailToFetchNotesIfNotesDoesNotExist()
        {

            //Act
            var response = await Handler.HandleAsync(Query);

            //Assert
            Assert.That(response.Errors.Single(), Is.EqualTo("Note doesn't exist"));
            Assert.That(response.NotSuccessful, Is.True);

        }

        [Test]
        public async Task SHouldFetchAllNotesSuccessfully()
        {
            //Arrange
            var note = new Note("Get it done");
            note.SetUser(new User("Adeola", "Adeolaaderibigbe@gmail.com"));
            Entities.Setup(s => s.Entities).Returns(new[] { note }.AsQueryable());

            //Act
            var response = await Handler.HandleAsync(Query);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(0));
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.Data, Is.TypeOf<NoteDTO>());
        }

        protected override FetchNoteByIdQuery CreateQuery()
        {
            return new FetchNoteByIdQuery() 
            { 
                TaskId = new Guid(),
                UserId = new Guid()
                
            };

        }
    }
}
