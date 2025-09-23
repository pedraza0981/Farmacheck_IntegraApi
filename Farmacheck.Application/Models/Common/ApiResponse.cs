using System.Collections.Generic;

namespace Farmacheck.Application.Models.Common;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }

    public T? Data { get; init; }

    public IEnumerable<string>? Errors { get; init; }
}
