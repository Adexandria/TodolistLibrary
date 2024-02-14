using System.Net;
using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services;
using TasksLibrary.Utilities;

namespace TasksLibrary.Client.Services
{
    public class CreateClientCommandHandler : CommandHandler<CreateClientCommand, DbContext<AccessManagement>, CreateUserDTO>
    {
        public override async Task<ActionResult<CreateUserDTO>> HandleCommand(CreateClientCommand command)
        {
            var isExist = await Dbcontext.Context.UserRepository.IsExist(command.Email);
            if (isExist)
            {
                return FailedOperation("This email already exists", HttpStatusCode.BadRequest);
            }

            var hashedPassword = PasswordManager.HashPassword(command.Password, out string salt);

            IUser newUser = MapCommand.MapToEntity<CreateClientCommand, IUser>(command);

            newUser.PasswordHash = hashedPassword;
            newUser.Salt = salt;

            await Dbcontext.Context.UserRepository.Add(newUser);

            var commitStatus = await Dbcontext.CommitAsync();
            if (commitStatus.NotSuccessful)
                return FailedOperation("Couldn't create new user");

            var createdUser = new CreateUserDTO()
            {
                Name = $"{newUser.FirstName} {newUser.LastName}",
                Email = newUser.Email
            };
            return SuccessfulOperation(createdUser);
        }
        public IPasswordManager PasswordManager { get; set; }
    }
}
