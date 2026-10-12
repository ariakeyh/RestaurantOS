namespace Common.Interfaces;

public interface IQueryableHelper
{
    Task<List<T>> RunQueryAsync<T>(IQueryable<T> query);
    Task<int> RunCountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);
}