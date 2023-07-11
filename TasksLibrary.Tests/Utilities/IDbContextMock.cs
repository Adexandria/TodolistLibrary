using Moq;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Tests.Utilities
{
    public interface IDbContextMock<TDbcontext>
        where TDbcontext : class, IDbContext
    {
        Mock<TDbcontext> Build();
    }
}
