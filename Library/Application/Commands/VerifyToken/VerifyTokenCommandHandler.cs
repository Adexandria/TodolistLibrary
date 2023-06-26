using TasksLibrary.Extensions;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Application.Commands.VerifyToken
{
    public class VerifyTokenCommandHandler : CommandHandler<VerifyTokenCommand, DbContext<AccessManagement>, string>
    {
        public override async Task<ActionResult<string>> HandleCommand(VerifyTokenCommand command)
        {
            var userId = Dbcontext.Context.AuthenTokenRepository.VerifyToken(command.AccessToken);

            if (string.IsNullOrEmpty(userId))
                return await Task.FromResult(FailedOperation("Invalid token", System.Net.HttpStatusCode.Unauthorized));

            return await Task.FromResult(SuccessfulOperation(userId));
        }
    }
}
