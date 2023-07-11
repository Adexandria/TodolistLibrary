using TasksLibrary.Application.Queries.FetchAllNotes;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Tests.Utilities;

namespace TasksLibrary.Tests.Application.Queries
{

    [TestFixture]
    public class FetchAllNotesTests : QueryHandlerTest<FetchAllNotesQuery,FetchAllNotesQueryHandler, QueryContext<Note>, Note,List<NoteDTO>>
    {
        [Test]
        public async Task ShouldFailToFetchNotesIfNotesDoesNotExist()
        {
           
            //Act
            var response = await Handler.HandleAsync(Query);

            //Assert
            Assert.That(response.Errors.Single(), Is.EqualTo("No notes found"));
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
            Assert.That(response.Data, Is.TypeOf<List<NoteDTO>>());
        }

        protected override FetchAllNotesQuery CreateQuery()
        {
            return new FetchAllNotesQuery
            {
                UserId = new Guid()
            };

        }
    }
}
