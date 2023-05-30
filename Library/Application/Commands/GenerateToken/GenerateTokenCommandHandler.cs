using TasksLibrary.Extensions;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services.Architecture.Database;

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
                return FailedOperation("Invalid User",System.Net.HttpStatusCode.BadRequest);

            var accessToken = Dbcontext.Context.AuthenTokenRepository.GenerateAccessToken(user);

            return SuccessfulOperation(accessToken);
        }
    }
}
