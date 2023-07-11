using Moq;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Utilities;

namespace TasksLibrary.Tests
{
    public static class TestHelper
    {
        public static void AssumeCommitSuccessfully<T>(this Mock<T> _dbMock) where T : class, IDbContext
        {
            _dbMock.Setup(s => s.CommitAsync()).ReturnsAsync(ActionResult.Successful());
        }

        public static void AssumeCommitFails<T>(this Mock<T> _dbMock) where T : class, IDbContext
        {
            _dbMock.Setup(s => s.CommitAsync()).ReturnsAsync(ActionResult.Failed());
        }
    }
}
