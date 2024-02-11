using TasksLibrary.Utilities;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Services;
using System.Security.Claims;

namespace TasksLibrary.Application.Commands.VerifyToken
{
    public class VerifyTokenCommandHandler : CommandHandler<VerifyTokenCommand, DbContext<AccessManagement>, UserDTO>
    {
        public override async Task<ActionResult<UserDTO>> HandleCommand(VerifyTokenCommand command)
        {
            var userClaims = Dbcontext.Context.AuthenTokenRepository.VerifyToken(command.AccessToken);

            if (userClaims == null)
                return await Task.FromResult(FailedOperation("Invalid token", System.Net.HttpStatusCode.Unauthorized));


            var user = new UserDTO(userClaims.FindFirst("id").Value, userClaims.FindFirst(ClaimTypes.Email).Value);


            return await Task.FromResult(SuccessfulOperation(user));
        }
    }
}
