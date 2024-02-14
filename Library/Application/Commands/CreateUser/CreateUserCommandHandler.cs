using System.Net;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Utilities;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services;
using Automappify.Services;
using System.Reflection;
using FluentNHibernate.Mapping;
using TasksLibrary.Architecture.Extensions;

namespace TasksLibrary.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, DbContext<AccessManagement>, CreateUserDTO>
    {
        public override async Task<ActionResult<CreateUserDTO>> HandleCommand(CreateUserCommand command)
        {
            var isExist = await Dbcontext.Context.UserRepository.IsExist(command.Email);
            if (isExist)
            {
                return FailedOperation("This email already exists", HttpStatusCode.BadRequest);
            }

            var hashedPassword = PasswordManager.HashPassword(command.Password, out string salt);

            IUser newUser = MapCommand.MapToEntity<CreateUserCommand, IUser>(command);

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

