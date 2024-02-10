using Moq;
using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services;
using TasksLibrary.Tests.Utilities;

namespace TasksLibrary.Tests.Application.Commands
{
    [TestFixture]
    public class CreateUserTests :CommandHandlerTest<CreateUserCommand,CreateUserCommandHandler,CreateUserDTO,DbContext<AccessManagement>,
                                AccessDbContextMock>
    {

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            PasswordManager = new Mock<IPasswordManager>().Object;
        }

        [Test]
        public void ShouldFailToValidate()
        {
            //Arrange
            var command = new CreateUserCommand();

            //Act
            var response = command.Validate();

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(3));
            Assert.That(response.IsSuccessful, Is.False);
        }

        [Test]
        public async Task ShouldFailToCreateUserIfUserEmailExist()
        {
            //Arrange
            DbContext.Setup(s => s.Context.UserRepository.IsExist(It.IsAny<string>())).ReturnsAsync(true);

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Errors.Single(), Is.EqualTo("This email already exists"));
        }

        [Test]
        public async Task ShouldFailToCreateUserIfCommitFails()
        {
            //Arrange
            Handler.PasswordManager = PasswordManager;
            DbContext.Setup(s => s.Context.UserRepository.Add(It.IsAny<User>()));
            DbContext.AssumeCommitFails();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Errors.Single(), Is.EqualTo("Couldn't create new user"));
        }

        [Test]
        public async Task ShouldCreateUserSuccessfully()
        {
            //Arrange
            Handler.PasswordManager = PasswordManager;
            DbContext.Setup(s => s.Context.UserRepository.Add(It.IsAny<User>()));
            DbContext.AssumeCommitSuccessfully();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors, Is.Empty);
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.Data, Is.TypeOf<CreateUserDTO>());
        }
        protected override CreateUserCommand CreateCommand()
        {
            return new CreateUserCommand
            {
                Email = "aderibigbe.adeola@gmail.com",
                Password = "Adeola5real",
                ConfirmPassword = "Adeola5real",
                FirstName = "Adeola"
            };
        }
        private IPasswordManager PasswordManager { get; set; }
    }
}
