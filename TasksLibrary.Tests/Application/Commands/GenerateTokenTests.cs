using Moq;
using TasksLibrary.Application.Commands.GenerateToken;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Tests.Utilities;

namespace TasksLibrary.Tests.Application.Commands
{
    [TestFixture]
    public class GenerateTokenTests : CommandHandlerTest<GenerateTokenCommand,GenerateTokenCommandHandler,string,DbContext<AccessManagement>,AccessDbContextMock>
    {

        [Test]
        public void ShouldFailToValidate()
        {
            //Arrange
            var command = new GenerateTokenCommand();

            //Act
            var response = command.Validate();

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
        }

        [Test]
        public async Task ShouldFailToGenerateTokenIfUserIdDoesNotExist()
        {
            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Errors.Single(), Is.EqualTo("Invalid refresh token"));

        }

        [Test]
        public async Task ShouldGenerateTokenSuccessfully()
        {
            //Arrange
            var user = new UserId(Guid.NewGuid());
            DbContext.Setup(s => s.Context.RefreshTokenRepository.GetUserByRefreshToken(Command.RefreshToken))
                .ReturnsAsync(user);
            DbContext.Setup(s => s.Context.UserRepository.GetExistingEntityById(It.IsAny<Guid>())).ReturnsAsync(new User("ade","ade", "ade"));

            DbContext.Setup(s => s.Context.AuthenTokenRepository.GenerateAccessToken(It.IsAny<Guid>(), It.IsAny<string>())).Returns("accessToken");

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(0));
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.Data, Is.EqualTo("accessToken"));

        }


        [Test]
        public async Task ShouldFailToGenerateTokenIfUserDoesNotExist()
        {
            //Arrange
            var user = new UserId(Guid.NewGuid());
            DbContext.Setup(s => s.Context.RefreshTokenRepository.GetUserByRefreshToken(Command.RefreshToken))
                .ReturnsAsync(user);

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Errors.Single(), Is.EqualTo("Invalid user"));

        }

        protected override GenerateTokenCommand CreateCommand()
        {
            return new GenerateTokenCommand
            {
                RefreshToken = "abcde"
            };
        }
    }
}
