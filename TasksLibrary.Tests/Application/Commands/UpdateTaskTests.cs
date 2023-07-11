using Moq;
using TasksLibrary.Application.Commands.UpdateTask;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Tests.Utilities;

namespace TasksLibrary.Tests.Application.Commands
{
    [TestFixture]
    public class UpdateTaskTests: CommandHandlerTest<UpdateTaskCommand,UpdateTaskCommandHandler,DbContext<TaskManagement>,TaskDbContextMock>
    {
        [Test]
        public void ShouldFailToValidate()
        {
            //Arrange
            var command = new UpdateTaskCommand();

            //Act
            var response = command.Validate();

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
        }

        [Test]
        public async Task ShouldFailToUpdateTaskIfTaskDoesNotExist()
        {
            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count(), Is.EqualTo(1));    
            Assert.That(response.NotSuccessful, Is.True);
            Assert.That(response.Errors.Single(), Is.EqualTo("Note doesn't exist"));
        }

        [Test]
        public async Task ShouldUpdateTaskSuccessfully()
        {
            //Arrange
            DbContext.Setup(s => s.Context.NoteRepository.GetExistingEntityById(It.IsAny<Guid>())).ReturnsAsync(new Note("Do it"));
            DbContext.Setup(s => s.Context.NoteRepository.Update(It.IsAny<Note>()));
            DbContext.AssumeCommitSuccessfully();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count(), Is.EqualTo(0));
            Assert.That(response.IsSuccessful, Is.True);
        }


        [Test]
        public async Task ShouldUpdateTaskSuccessfullyIfDescriptionIsEmpty()
        {
            //Arrange
            DbContext.Setup(s => s.Context.NoteRepository.GetExistingEntityById(It.IsAny<Guid>())).ReturnsAsync(new Note("Do it"));
            DbContext.Setup(s => s.Context.NoteRepository.Update(It.IsAny<Note>()));
            DbContext.AssumeCommitSuccessfully();
            Command.Description = string.Empty;
            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count(), Is.EqualTo(0));
            Assert.That(response.IsSuccessful, Is.True);
        }

        [Test]
        public async Task ShouldFailToUpdateTaskIfCommitFails()
        {
            //Arrange
            DbContext.Setup(s => s.Context.NoteRepository.GetExistingEntityById(It.IsAny<Guid>())).ReturnsAsync(new Note("Do it"));
            DbContext.Setup(s => s.Context.NoteRepository.Update(It.IsAny<Note>()));
            DbContext.AssumeCommitFails();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count(), Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Errors.Single(), Is.EqualTo("Failed to update note"));
        }


        protected override UpdateTaskCommand CreateCommand()
        {
            return new UpdateTaskCommand() 
            {
                Description = "This is a description",
                Task = "This is a note 1"
            };

        }
    }
}
