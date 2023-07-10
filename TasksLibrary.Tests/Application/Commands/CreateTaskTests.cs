using Moq;
using TasksLibrary.Application.Commands.CreateTask;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Tests.DB;

namespace TasksLibrary.Tests.Application.Commands
{
    [TestFixture]
    public class CreateTaskTests : CommandHandlerTest<CreateTaskCommand,CreateTaskCommandHandler,Guid,DbContext<TaskManagement>,DbContextMock>
    {
        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            Command.Validate();
        }
        [Test]
        public async Task ShouldCreateTaskSuccessfully()
        {
            //Arrange
            DbContext.Setup(s => s.Context.UserRepository.GetExistingEntityById(Command.UserId))
                .ReturnsAsync(new User("Name", "adeolaaderibigbe09@gmail.com"));

            DbContext.Setup(s => s.Context.NoteRepository.Add(It.IsAny<Note>()));

            DbContext.AssumeCommitSuccessfully();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors, Is.Empty);
            Assert.That(response.Data, Is.TypeOf<Guid>());
        }
        protected override CreateTaskCommand CreateCommand()
        {
            return new CreateTaskCommand()
            {
                Task = "Do your laundry",
                UserId = Guid.NewGuid(),
                Description = "Clean your dirty clothes"
            };
        }
    }
}
