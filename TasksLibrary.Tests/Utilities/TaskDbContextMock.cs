using Moq;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Tests.Utilities
{
    public class TaskDbContextMock : IDbContextMock<DbContext<TaskManagement>>
    {
        public Mock<DbContext<TaskManagement>> Build()
        {
            var builder = new Mock<DbContext<TaskManagement>>();
            builder.Setup(s => s.Context.UserRepository).Returns(new Mock<IUserRepository>().Object);
            builder.Setup(s => s.Context.NoteRepository).Returns(new Mock<INoteRepository>().Object);

            return builder;
        }
    }
}
