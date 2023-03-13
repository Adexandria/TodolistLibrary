using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Extensions;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Application.Commands.Login
{
    public class LoginCommandHandler: CommandHandler<LoginCommand,AccessRepository,LoginDTO>
    {
        public override async Task<ActionResult<LoginDTO>> HandleCommand()
        {
            var authenticatedUser = DbContext.UserRepository.AuthenticateUser(command.Email, command.Password);
            if(authenticatedUser == null)
            {
                return new ActionResult<LoginDTO>().FailedOperation("Invalid password or password", (int)HttpStatusCode.BadRequest);
            }
            AccessToken accessToken;

            if(authenticatedUser.AccessToken != null && authenticatedUser.RefreshToken !=null)
            {
                
            }

            
            
                
        }
    }
}
