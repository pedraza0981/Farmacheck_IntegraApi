using Farmacheck.Application.Models.Zones;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface IZoneApiClient
    {
        Task<IEnumerable<ZoneResponse>> GetAllZonesAsync();
        Task<List<ZoneResponse>> GetZonesAsync();
        Task<PaginatedResponse<ZoneResponse>> GetZonesByPageAsync(int page, int items);
        Task<ZoneResponse?> GetZoneAsync(int id);
        Task<int> CreateAsync(ZoneRequest request);
        Task<bool> UpdateAsync(UpdateZoneRequest request);
        Task DeleteAsync(int id);
    }
}
