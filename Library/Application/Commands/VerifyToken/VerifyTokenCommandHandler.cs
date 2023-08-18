using TasksLibrary.Utilities;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Services;

namespace TasksLibrary.Application.Commands.VerifyToken
{
    public class VerifyTokenCommandHandler : CommandHandler<VerifyTokenCommand, DbContext<AccessManagement>, UserDTO>
    {
        public override async Task<ActionResult<UserDTO>> HandleCommand(VerifyTokenCommand command)
        {
            var user = Dbcontext.Context.AuthenTokenRepository.VerifyToken(command.AccessToken);

            if (user == null)
                return await Task.FromResult(FailedOperation("Invalid token", System.Net.HttpStatusCode.Unauthorized));

            return await Task.FromResult(SuccessfulOperation(user));
        }
    }
}
