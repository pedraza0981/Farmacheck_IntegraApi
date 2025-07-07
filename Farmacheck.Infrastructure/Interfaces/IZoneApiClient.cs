using Farmacheck.Infrastructure.Models.Zones;

namespace Farmacheck.Infrastructure.Interfaces
{
    public interface IZoneApiClient
    {
        Task<List<ZoneResponse>> GetZonesAsync();
        Task<List<ZoneResponse>> GetZonesByPageAsync(int page, int items);
        Task<ZoneResponse?> GetZoneAsync(int id);
        Task<int> CreateAsync(ZoneRequest request);
        Task<bool> UpdateAsync(UpdateZoneRequest request);
        Task DeleteAsync(int id);
    }
}
