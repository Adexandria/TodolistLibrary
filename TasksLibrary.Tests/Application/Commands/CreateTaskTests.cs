﻿using Moq;
using TasksLibrary.Application.Commands.CreateTask;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Tests.Utilities;

namespace TasksLibrary.Tests.Application.Commands
{
    [TestFixture]
    public class CreateTaskTests : CommandHandlerTest<CreateTaskCommand,CreateTaskCommandHandler,Guid,DbContext<TaskManagement>,TaskDbContextMock>
    {

        [Test]
        public void ShouldFailToValidate()
        {
            //Arrange
            var command = new CreateTaskCommand();

            //Act
            var response = command.Validate();

            //Assert
            Assert.That(response.Errors.Count, Is.EqualTo(1));
            Assert.That(response.IsSuccessful, Is.False);
        }



        [Test]
        public async Task ShouldCreateTaskSuccessfully()
        {
            //Arrange
            DbContext.Setup(s => s.Context.UserRepository.GetExistingEntityById(Command.UserId))
                .ReturnsAsync(new User("Name", "adeolaaderibigbe09@gmail.com"));

            DbContext.Setup(s => s.Context.NoteRepository.Add(It.IsAny<Note>()));

            DbContext.AssumeCommitSuccessfully();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors, Is.Empty);
            Assert.That(response.Data, Is.TypeOf<Guid>());
        }

        [Test]
        public async Task ShouldCreateTaskSuccessfullyIfDescriptionIsEmpty()
        {
            //Arrange
            DbContext.Setup(s => s.Context.UserRepository.GetExistingEntityById(Command.UserId))
                .ReturnsAsync(new User("Name", "adeolaaderibigbe09@gmail.com"));

            DbContext.Setup(s => s.Context.NoteRepository.Add(It.IsAny<Note>()));

            DbContext.AssumeCommitSuccessfully();
            Command.Description = string.Empty;

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors, Is.Empty);
            Assert.That(response.Data, Is.TypeOf<Guid>());
        }


        [Test]
        public async Task ShouldFailToCreateIfUserDoesNotExist()
        {
            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Single(), Is.EqualTo("User doesn't exist"));
        }

        [Test]
        public async Task ShouldFailToCreateTaskIfCommitFails()
        {
            //Arrange
            DbContext.Setup(s => s.Context.UserRepository.GetExistingEntityById(Command.UserId))
                .ReturnsAsync(new User("Name", "adeolaaderibigbe09@gmail.com"));

            DbContext.Setup(s => s.Context.NoteRepository.Add(It.IsAny<Note>()));

            DbContext.AssumeCommitFails();

            //Act
            var response = await Handler.HandleCommand(Command);

            //Assert
            Assert.That(response.Errors.Single(), Is.EqualTo("Failed to create task"));
        }

        protected override CreateTaskCommand CreateCommand()
        {
            return new CreateTaskCommand()
            {
                Task = "Do your laundry",
                UserId = Guid.NewGuid(),
                Description = "Clean your dirty clothes"
            };
        }
    }
}
