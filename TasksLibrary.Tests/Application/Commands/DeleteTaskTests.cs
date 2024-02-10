using Moq;
using TasksLibrary.Application.Commands.DeleteTask;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Tests.Utilities;

namespace TasksLibrary.Tests.Application.Commands
{
    [TestFixture]
    public class DeleteTaskTests : CommandHandlerTest<DeleteTaskCommand,DeleteTaskCommandHandler,DbContext<TaskManagement>, TaskDbContextMock>
    {

        [Test]
        public async Task ShouldFailToDeleteIfTaskDoesNotExist()
        {

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Errors.Single(), Is.EqualTo("Note doesn't exist"));
        }

        [Test]
        public async Task ShouldFailToDeleteIfCommitFails()
        {
            //Arrange
            var note = new Note("Get it done");
            DbContext.Setup(s => s.Context.NoteRepository.GetExistingEntityById(new Guid())).ReturnsAsync(note);
            DbContext.AssumeCommitFails();
            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Errors.Single(), Is.EqualTo("Failed to delete note"));
        }

        [Test]
        public async Task ShouldDeleteTaskSuccessfully()
        {
            //Arrange
            var note = new Note("Get it done");
            DbContext.Setup(s => s.Context.NoteRepository.GetExistingEntityById(new Guid())).ReturnsAsync(note);
            DbContext.AssumeCommitSuccessfully();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors, Is.Empty);
            Assert.That(response.IsSuccessful, Is.True);
        }

        protected override DeleteTaskCommand CreateCommand()
        {
            return new DeleteTaskCommand() 
            { 
                TaskId = new Guid()
            };

        }
    }
}
