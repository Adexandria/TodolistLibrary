using System.Net;
using TasksLibrary.Utilities;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Architecture.Database;
using System.Security.Claims;

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


            if (authenticatedUser.AccessTokenId  != null && authenticatedUser.RefreshTokenId != null)
            {
                await Dbcontext.Context.RefreshTokenRepository.Delete(authenticatedUser.RefreshTokenId.Id);
                await Dbcontext.Context.AccessTokenRepository.Delete(authenticatedUser.AccessTokenId.Id);
            }


            var claims = new Dictionary<string, object>
            {
                { "id", authenticatedUser.Id.ToString("N") },

                { ClaimTypes.Email, authenticatedUser.Email }
            };

            string accessToken =  Dbcontext.Context.AuthenTokenRepository.GenerateAccessToken(claims);
            string refreshToken = Dbcontext.Context.AuthenTokenRepository.GenerateRefreshToken();

            var accessTokenModel = new AccessToken(accessToken, new UserId(authenticatedUser.Id));
            var refreshTokenModel = new RefreshToken(refreshToken, DateTime.Now.AddDays(1), new UserId(authenticatedUser.Id));

            await Dbcontext.Context.RefreshTokenRepository.Add(refreshTokenModel);

            await Dbcontext.Context.AccessTokenRepository.Add(accessTokenModel);

            authenticatedUser.AccessTokenId = new AccessTokenId(accessTokenModel.Id);

            authenticatedUser.RefreshTokenId = new RefreshTokenId(accessTokenModel.Id);

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
