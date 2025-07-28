using System;
using System.Collections.Generic;

namespace Farmacheck.Application.Models.Common;

public sealed class PaginatedResponse<T>(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
    where T : class
{
    public IEnumerable<T> Items { get; init; } = items;
    public int TotalCount { get; init; } = totalCount;
    public int CurrentPage { get; init; } = currentPage;
    public int PageSize { get; init; } = pageSize;
    public bool HasNextPage => Math.Ceiling((decimal)TotalCount / PageSize) > CurrentPage;
}
