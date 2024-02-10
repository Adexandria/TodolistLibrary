using System.Net;
using TasksLibrary.Utilities;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Application.Commands.Login
{
    public class LoginCommandHandler: CommandHandler<LoginCommand,DbContext<AccessManagement>,LoginDTO>
    {
        public override async Task<ActionResult<LoginDTO>> HandleCommand(LoginCommand command)
        {
            var authenticatedUser = await Dbcontext.Context.UserRepository.AuthenticateUser(command.Email, command.Password);
            if (authenticatedUser == null)
            {
                return FailedOperation("Invalid password or email", HttpStatusCode.BadRequest);
            }
         

            if (authenticatedUser.AccessToken != null && authenticatedUser.RefreshToken != null)
            {
               await Dbcontext.Context.RefreshTokenRepository.Delete(authenticatedUser.RefreshToken);
               await Dbcontext.Context.AccessTokenRepository.Delete(authenticatedUser.AccessToken);
            }

            string accessToken =  Dbcontext.Context.AuthenTokenRepository.GenerateAccessToken(authenticatedUser.Id,authenticatedUser.Email);
            string refreshToken = Dbcontext.Context.AuthenTokenRepository.GenerateRefreshToken();

            var accessTokenModel = new AccessToken(accessToken, new UserId(authenticatedUser.Id));
            var refreshTokenModel = new RefreshToken(refreshToken, DateTime.Now.AddDays(1), new UserId(authenticatedUser.Id));

            authenticatedUser.AccessToken = accessTokenModel;   
            authenticatedUser.RefreshToken = refreshTokenModel;

            await Dbcontext.Context.UserRepository.Update(authenticatedUser);

            var loginDTO = new LoginDTO()
            {
                Email = authenticatedUser.Email,
                Name = $"{authenticatedUser.FirstName} {authenticatedUser.LastName}",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            var commitStatus = await Dbcontext.CommitAsync();
            if (commitStatus.NotSuccessful)
                return FailedOperation("Failed to login user");

            return SuccessfulOperation(loginDTO);

        }
    }
}
