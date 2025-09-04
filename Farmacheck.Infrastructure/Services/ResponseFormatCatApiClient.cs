using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.ResponseFormat;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class ResponseFormatCatApiClient : IResponseFormatCatApiClient
    {
        private readonly HttpClient _http;

        public ResponseFormatCatApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ResponseFormatCatResponse>> GetAllFormatsAsync()
        {
            var formats = await _http.GetFromJsonAsync<IEnumerable<ResponseFormatCatResponse>>("api/v1/responseformat")
                   ?? Enumerable.Empty<ResponseFormatCatResponse>();

            return formats;
        }
    }
}

