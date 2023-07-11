using Moq;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Tests.Utilities
{
    public class AccessDbContextMock : IDbContextMock<DbContext<AccessManagement>>
    {
        public Mock<DbContext<AccessManagement>> Build()
        {
            var builder = new Mock<DbContext<AccessManagement>>();
            builder.Setup(s => s.Context.UserRepository).Returns(new Mock<IUserRepository>().Object);
            builder.Setup(s => s.Context.RefreshTokenRepository).Returns(new Mock<IRefreshTokenRepository>().Object);
            builder.Setup(s => s.Context.AccessTokenRepository).Returns(new Mock<IAccessTokenRepository>().Object);
            builder.Setup(s => s.Context.AuthenTokenRepository).Returns(new Mock<IAuthToken>().Object);
            return builder;
        }
    }
}
