using Moq;
using TasksLibrary.Application.Commands.Login;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Tests.Utilities;

namespace TasksLibrary.Tests.Application.Commands
{
    [TestFixture]
    public class LoginCommandTests : CommandHandlerTest<LoginCommand,LoginCommandHandler,LoginDTO,DbContext<AccessManagement>,AccessDbContextMock>
    {
        [Test]
        public void ShouldFailToValidate()
        {
            //Arrange
            var command = new LoginCommand();

            //Act
            var response = command.Validate();

            //Assert
            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(2));
            Assert.That(response.IsSuccessful, Is.False);
        }


        [Test]
        public async Task ShouldFailToAuthenticateUserIfPasswordOrEmailIsWrong()
        {

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Errors.Single(), Is.EqualTo("Invalid password or email"));
        }

        [Test]
        public async Task ShouldFailToAuthicateUserIfCommitFails()
        {
            //Arrange
            var user = new User("ade", "ade");
            user.Id = Guid.NewGuid();
            user.AccessToken = new AccessToken("accessToken", new UserId(user.Id));
            user.RefreshToken = new RefreshToken("refreshToken", DateTime.Now.AddDays(3), new UserId(user.Id));
            DbContext.Setup(s => s.Context.UserRepository.AuthenticateUser(Command.Email, Command.Password)).ReturnsAsync(user);
            DbContext.Setup(s => s.Context.RefreshTokenRepository.Delete(It.IsAny<RefreshToken>()));
            DbContext.Setup(s => s.Context.AccessTokenRepository.Delete(It.IsAny<AccessToken>()));
            DbContext.Setup(s => s.Context.UserRepository.Update(It.IsAny<User>()));
            DbContext.Setup(s => s.Context.AuthenTokenRepository.GenerateRefreshToken()).Returns("refreshToken");
            DbContext.Setup(s => s.Context.AuthenTokenRepository.GenerateAccessToken(user)).Returns("accessToken");
            DbContext.AssumeCommitFails();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Errors.Single(), Is.EqualTo("Failed to login user"));
        }


        [Test]
        public async Task ShouldAuthicateUserSuccessfully()
        {
            //Arrange
            var user = new User("ade", "ade");
            user.Id = Guid.NewGuid();
            user.AccessToken = new AccessToken("accessToken", new UserId(user.Id));
            user.RefreshToken = new RefreshToken("refreshToken", DateTime.Now.AddDays(3), new UserId(user.Id));
            DbContext.Setup(s => s.Context.UserRepository.AuthenticateUser(Command.Email, Command.Password)).ReturnsAsync(user);
            DbContext.Setup(s => s.Context.RefreshTokenRepository.Delete(It.IsAny<RefreshToken>()));
            DbContext.Setup(s => s.Context.AccessTokenRepository.Delete(It.IsAny<AccessToken>()));
            DbContext.Setup(s => s.Context.UserRepository.Update(It.IsAny<User>()));
            DbContext.Setup(s => s.Context.AuthenTokenRepository.GenerateRefreshToken()).Returns("refreshToken");
            DbContext.Setup(s => s.Context.AuthenTokenRepository.GenerateAccessToken(user)).Returns("accessToken");
            DbContext.AssumeCommitSuccessfully();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(0));
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.Data, Is.TypeOf<LoginDTO>());
        }

        [Test]
        public async Task ShouldAuthicateUserSuccessfullyIfFirstTimeLogin()
        {
            //Arrange
            var user = new User("ade", "ade");
            user.Id = Guid.NewGuid();
            DbContext.Setup(s => s.Context.UserRepository.AuthenticateUser(Command.Email, Command.Password)).ReturnsAsync(user);
            DbContext.Setup(s => s.Context.UserRepository.Update(It.IsAny<User>()));
            DbContext.AssumeCommitSuccessfully();
            DbContext.Setup(s => s.Context.AuthenTokenRepository.GenerateRefreshToken()).Returns("refreshToken");
            DbContext.Setup(s => s.Context.AuthenTokenRepository.GenerateAccessToken(user)).Returns("accessToken");

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(0));
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.Data, Is.TypeOf<LoginDTO>());
        }
        protected override LoginCommand CreateCommand()
        {
            return new LoginCommand 
            { 
                Email = "adeolaaderibigbe@gmail.com",
                Password ="Adeola5real"
            };

        }
    }
}
