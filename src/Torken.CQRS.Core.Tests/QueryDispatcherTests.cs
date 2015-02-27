using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Torken.CQRS.Interfaces;

namespace Torken.CQRS.Core.Tests
{
    [TestClass]
    public class QueryDispatcherTests
    {
        public class TestQuery : IQuery
        {
        }

        public class TestQueryResult : IQueryResult
        { }

        [TestMethod]
        public void QueryHandlerNotFound()
        {
            var serviceProviderMoq = new Moq.Mock<IServiceProvider>();

            var queryDispatcher = new QueryDispatcher(serviceProviderMoq.Object);

            Action action = () => queryDispatcher.Send<TestQueryResult, TestQuery>(new TestQuery());

            action.ShouldThrow<Exceptions.QueryHandlerNotFound>();

            action = () => queryDispatcher.Send<TestQueryResult>();

            action.ShouldThrow<Exceptions.QueryHandlerNotFound>();
        }

        [TestMethod]
        public void QueryHandlerFound()
        {
            var query = new TestQuery();
            var queryResult = new TestQueryResult();

            var queryHandlerMoq = new Moq.Mock<IQueryHandler<TestQueryResult, TestQuery>>();
            queryHandlerMoq.Setup(handler => handler.Execute(query)).Returns(queryResult);

            var simpleQueryHandlerMoq = new Moq.Mock<IQueryHandler<TestQueryResult>>();
            simpleQueryHandlerMoq.Setup(handler => handler.Execute()).Returns(queryResult);

            var serviceProviderMoq = new Moq.Mock<IServiceProvider>();
            serviceProviderMoq.Setup(s => s.GetService(typeof(IQueryHandler<TestQueryResult>))).Returns(simpleQueryHandlerMoq.Object);
            serviceProviderMoq.Setup(s => s.GetService(typeof(IQueryHandler<TestQueryResult, TestQuery>))).Returns(queryHandlerMoq.Object);

            var queryDispatcher = new QueryDispatcher(serviceProviderMoq.Object);

            queryDispatcher.Send<TestQueryResult>().Should().BeSameAs(queryResult);

            queryDispatcher.Send<TestQueryResult, TestQuery>(query).Should().BeSameAs(queryResult);
        }
    }
}