using Moq;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Tests.DB
{
    public interface IDbContextMock<TDbcontext>
        where TDbcontext : class, IDbContext
    {
        Mock<TDbcontext> Build();
    }
}
