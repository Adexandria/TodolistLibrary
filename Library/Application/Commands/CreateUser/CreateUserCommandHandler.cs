﻿using System.Net;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Utilities;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services;

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

            var newUser = new User(command.Name, command.Email);
            var hashedPassword = PasswordManager.HashPassword(command.Password, out var salt);
            newUser.Salt = salt;
            newUser.PasswordHash = hashedPassword;

            await Dbcontext.Context.UserRepository.Add(newUser);
            var commitStatus = await Dbcontext.CommitAsync();
            if (commitStatus.NotSuccessful)
                return FailedOperation("Couldn't create new user");

            var createdUser = new CreateUserDTO()
            {
                Name = newUser.Name,
                Email = newUser.Email
            };
            return SuccessfulOperation(createdUser);
        }
        public IPasswordManager PasswordManager { get; set; }
    }
}

