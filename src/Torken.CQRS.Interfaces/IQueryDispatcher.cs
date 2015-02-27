namespace Torken.CQRS.Interfaces
{
    public interface IQueryDispatcher
    {
        TQueryResult Send<TQueryResult, TQuery>(TQuery query)
            where TQueryResult : IQueryResult
            where TQuery : IQuery;

        TQueryResult Send<TQueryResult>()
            where TQueryResult : IQueryResult;
    }
}