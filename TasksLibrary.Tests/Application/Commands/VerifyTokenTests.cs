using TasksLibrary.Application.Commands.VerifyToken;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Tests.Utilities;

namespace TasksLibrary.Tests.Application.Commands
{
    [TestFixture]
    public class VerifyTokenTests : CommandHandlerTest<VerifyTokenCommand,VerifyTokenCommandHandler,string, DbContext<AccessManagement>,AccessDbContextMock>
    {

        [Test]
        public void ShouldFailToValidate()
        {
            //Arrange
            var command = new VerifyTokenCommand();

            //Act
            var response = command.Validate();

            //Assert
            Assert.That(response.NotSuccessful, Is.True);
            Assert.That(response.Errors.Count,Is.EqualTo(1));
        }


        [Test]
        public async Task ShouldFailToVerifyTokenIfTokenIsInvalid()
        {
            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.NotSuccessful,Is.True);
            Assert.That(response.Errors.Single(), Is.EqualTo("Invalid token"));
            
        }

        [Test]
        public async Task ShoulVerifyTokenSuccessfully()
        {
            //Arrange
            DbContext.Setup(s => s.Context.AuthenTokenRepository.VerifyToken(Command.AccessToken)).Returns("userId");

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.Data,Is.TypeOf<string>());
        }

        protected override VerifyTokenCommand CreateCommand()
        {
            return new VerifyTokenCommand()
            {
                AccessToken = "accessToken"
            };
        }
    }
}
