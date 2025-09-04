using Farmacheck.Application.Models.ResponseFormat;

namespace Farmacheck.Application.Interfaces
{
    public interface IResponseFormatCatApiClient
    {
        Task<IEnumerable<ResponseFormatCatResponse>> GetAllFormatsAsync();
    }
}
