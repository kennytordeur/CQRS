namespace Torken.CQRS.Core
{
    using System;
    using Torken.CQRS.Interfaces;

    public class QueryDispatcher : AbstractQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override IQueryHandler<TQueryResult> ResolveQueryHandler<TQueryResult>()
        {
            return _serviceProvider.GetService(typeof(IQueryHandler<TQueryResult>)) as IQueryHandler<TQueryResult>;
        }

        protected override IQueryHandler<TQueryResult, TQueryParameter> ResolveQueryHandler<TQueryResult, TQueryParameter>()
        {
            return _serviceProvider.GetService(typeof(IQueryHandler<TQueryResult, TQueryParameter>)) as IQueryHandler<TQueryResult, TQueryParameter>;
        }
    }
}