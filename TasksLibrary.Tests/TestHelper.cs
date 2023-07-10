using Moq;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Utilities;

namespace TasksLibrary.Tests
{
    public static class TestHelper
    {
        public static void AssumeCommitSuccessfully(this Mock<DbContext<TaskManagement>> _dbMock)
        {
            _dbMock.Setup(s => s.CommitAsync()).ReturnsAsync(ActionResult.Successful());
        }
    }
}
