namespace Torken.CQRS.Core
{
    using Torken.CQRS.Core.Exceptions;
    using Torken.CQRS.Interfaces;

    public abstract class AbstractQueryDispatcher : IQueryDispatcher
    {
        protected abstract IQueryHandler<TQueryResult> ResolveQueryHandler<TQueryResult>() where TQueryResult : IQueryResult;

        protected abstract IQueryHandler<TQueryResult, TQuery> ResolveQueryHandler<TQueryResult, TQuery>()
            where TQueryResult : IQueryResult
            where TQuery : IQuery;

        public TQueryResult Send<TQueryResult, TQuery>(TQuery query)
            where TQueryResult : IQueryResult
            where TQuery : IQuery
        {
            var queryHandler = ResolveQueryHandler<TQueryResult, TQuery>();

            if (null == queryHandler)
                throw new QueryHandlerNotFound();

            return queryHandler.Execute(query);
        }

        public TQueryResult Send<TQueryResult>() where TQueryResult : IQueryResult
        {
            var queryHandler = ResolveQueryHandler<TQueryResult>();

            if (null == queryHandler)
                throw new QueryHandlerNotFound();

            return queryHandler.Execute();
        }
    }
}