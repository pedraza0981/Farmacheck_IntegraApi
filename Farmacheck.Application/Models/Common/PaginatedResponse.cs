using System;
using System.Collections.Generic;

namespace Farmacheck.Application.Models.Common;

public sealed class PaginatedResponse<T> where T : class
{
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
    public int TotalCount { get; init; }
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }

    // Estos dos Vienen del API; los dejamos como init para respetar lo que mande
    public int TotalPages { get; init; }
    public bool HasNextPage { get; init; }
    public bool HasPreviousPage { get; init; }
}