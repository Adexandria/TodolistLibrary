using TasksLibrary.Utilities;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Application.Commands.GenerateToken
{
    public class GenerateTokenCommandHandler : CommandHandler<GenerateTokenCommand, DbContext<AccessManagement>, string>
    {
        public override async Task<ActionResult<string>> HandleCommand(GenerateTokenCommand command)
        {
           var userId = await Dbcontext.Context.RefreshTokenRepository.GetUserByRefreshToken(command.RefreshToken);

            if (userId == null)
                return FailedOperation("Invalid refresh token",System.Net.HttpStatusCode.BadRequest);

            var user = await Dbcontext.Context.UserRepository.GetExistingEntityById(userId.Id);

            if (user == null)
                return FailedOperation("Invalid user",System.Net.HttpStatusCode.NotFound);

            var accessToken = Dbcontext.Context.AuthenTokenRepository.GenerateAccessToken(user.Id,user.Email);

            return SuccessfulOperation(accessToken);
        }
    }
}
