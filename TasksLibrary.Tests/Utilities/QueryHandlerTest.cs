using Moq;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Utilities;

namespace TasksLibrary.Tests.Utilities
{
    public abstract class QueryHandlerTest<TQuery, THandler, TQueryContext, TDomain,TResponse> : QueryHandlerTest<TQueryContext, TDomain>
        where TQuery : Query<TResponse>
        where THandler : QueryHandler<TQuery,TQueryContext, TResponse> , new()
        where TQueryContext : QueryContext<TDomain>
    { 
        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            Query = CreateQuery();
            Handler = new THandler()
            {
                QueryContext = Entities.Object    
            };
        }

        [Test]
        public void ShouldValidate()
        {
            //Act
            var response = Query.Validate();

            //Assert
            Assert.That(response.Errors, Is.Empty);
            Assert.That(response.IsSuccessful, Is.True);
        }

        protected virtual TQuery CreateQuery()
        {
            return new Mock<TQuery>().Object;
        }
        protected TQuery Query { get; set; }
        protected THandler Handler { get; set; }
    }


    public abstract class QueryHandlerTest<TQueryContext,TDomain> 
        where TQueryContext : QueryContext<TDomain>
    {
        [SetUp]
        protected virtual void SetUp()
        {
            Entities = new Mock<TQueryContext>();
        }
        protected Mock<TQueryContext> Entities { get; set; }
    }
}
