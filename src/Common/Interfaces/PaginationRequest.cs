using MediatR;

namespace Common.Interfaces;

public abstract record PaginationRequest<TResult>  : IRequest<Pagination<TResult>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}