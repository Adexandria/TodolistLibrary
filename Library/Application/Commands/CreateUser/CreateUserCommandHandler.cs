using System.Net;
using TasksLibrary.Extensions;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services.Architecture.Database;

namespace TasksLibrary.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, DbContext<AccessRepository>, CreateUserDTO>
    {
        public override async Task<ActionResult<CreateUserDTO>> HandleCommand(CreateUserCommand command)
        {
            var isExist = await Dbcontext.Context.UserRepository.IsExist(command.Email);
            if (isExist)
            {
                return FailedOperation("The email already exists", HttpStatusCode.BadRequest);
            }

            var newUser = new User(command.Name, command.Email);
            var hashedPassword = newUser.HashPassword(command.Password, out var salt);
            newUser.Salt = salt;
            newUser.PasswordHash = hashedPassword;

            await Dbcontext.Context.UserRepository.Add(newUser);
            var commitStatus = await Dbcontext.CommitAsync();
            if (commitStatus.NotSuccessful)
                return FailedOperation("Couldn't create new user", HttpStatusCode.BadRequest);

            var createdUser = new CreateUserDTO()
            {
                Name = newUser.Name,
                Email = newUser.Email
            };
            return SuccessfulOperation(createdUser);
        }
    }
}

