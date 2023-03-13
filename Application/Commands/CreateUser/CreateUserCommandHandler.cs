using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Extensions;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, AccessRepository, CreateUserDTO>
    {
        public override async Task<ActionResult<CreateUserDTO>> HandleCommand()
        {
            var isExist = await DbContext.UserRepository.IsExist(command.Email);
            if (isExist)
            {
                return new ActionResult<CreateUserDTO>().FailedOperation("The email already exists",(int)HttpStatusCode.BadRequest);
            }

            var newUser = new User(command.Name, command.Email);
            var salt = string.Empty;
            var hashedPassword = newUser.HashPassword(command.Password,out salt);
            newUser.Salt = salt;
            newUser.PasswordHash = hashedPassword;
            
            DbContext.UserRepository.Add(newUser);
            var isSuccessful = await DbContext.UserRepository.CommitAsync();
            if (!isSuccessful)
            {
                return new ActionResult<CreateUserDTO>().FailedOperation("Couldn't create new user", (int)HttpStatusCode.BadRequest);
            }

            var createdUser = new CreateUserDTO()
            {
                Name = newUser.Name,
                Email = newUser.Email
            };
            return new ActionResult<CreateUserDTO>().SuccessfulOperation(createdUser);
        }
    }
}

