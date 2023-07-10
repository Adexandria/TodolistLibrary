using Moq;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Utilities;

namespace TasksLibrary.Tests.DB
{


    public abstract class CommandHandlerTest<TCommand, THandler, TResponse, TDbContext, TDbContextMock> : CommandHandlerTest<TCommand,TDbContext, TDbContextMock>
        where TCommand : Command<TResponse>
        where TDbContext : class, IDbContext
        where TDbContextMock : IDbContextMock<TDbContext>
        where THandler : CommandHandler<TCommand, TDbContext, TResponse>, new()
    {
        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            Handler = new THandler
            {
                Dbcontext = DbContext.Object
            };
        }
        protected THandler Handler { get; set; }
    }


    public abstract class CommandHandlerTest<TCommand, THandler, TDbContext, TDbContextMock> : CommandHandlerTest<TCommand,TDbContext,TDbContextMock>
         where TCommand : Command
        where TDbContext : class, IDbContext
        where TDbContextMock : IDbContextMock<TDbContext>
        where THandler : CommandHandler<TCommand, TDbContext> , new()
    {
        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            Handler = new THandler
            { 
                Dbcontext = DbContext.Object
            };

        }
        protected THandler Handler { get; set; }
    }


    public abstract class CommandHandlerTest<TCommand,TDbContext,TDbContextMock> 
        where TCommand : Command
        where TDbContext : class, IDbContext
        where TDbContextMock : IDbContextMock<TDbContext>
    {

        [SetUp]
        protected virtual void SetUp()
        {
            Command = CreateCommand();
            DbContext =  Activator.CreateInstance<TDbContextMock>().Build();
        }

        protected virtual TCommand CreateCommand()
        {
            return new Mock<TCommand>().Object;
        }

        protected TCommand Command { get; set; }
        public Mock<TDbContext> DbContext { get; set; }
    }

  


}
