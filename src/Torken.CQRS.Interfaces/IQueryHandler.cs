namespace Torken.CQRS.Interfaces
{
    public interface IQueryHandler<TQueryResult, TQuery>
        where TQueryResult : IQueryResult
        where TQuery : IQuery
    {
        TQueryResult Execute(TQuery query);
    }

    public interface IQueryHandler<TQueryResult>
        where TQueryResult : IQueryResult
    {
        TQueryResult Execute();
    }
}