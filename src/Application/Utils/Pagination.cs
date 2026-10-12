using Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Utils;

public static class PaginationExtensions
{
    public static async Task<Pagination<T>> ToPaginationAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < 1)
            pageSize = 10; 

        var totalItems = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new Pagination<T>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems,
            Items = items,
        };
    }
    
    public static async Task<Pagination<T>> ToPagedList<T>(this List<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageIndex < 1)
            pageIndex = 1;

        if (pageSize < 1)
            pageSize = 10; 
        
        var totalItems = source.Count;

        var items = await Task.Run(() =>
            source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList()
        );

        return new Pagination<T>
        {
            PageNumber = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems,
            Items = items,
        };
    }
}