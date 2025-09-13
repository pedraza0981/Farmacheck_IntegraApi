
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.MailingProgramacion;
using Farmacheck.Application.Models.ZonaHorario;

namespace Farmacheck.Application.Interfaces
{
    public interface IMailingProgramacionClient
    {
        Task<IEnumerable<vMailingProgramacionWebResponse>> GetAllAsync();
        Task<PaginatedResponse<vMailingProgramacionWebResponse>> GetByPageAsync(int page, int items);
        Task<vMailingProgramacionWebResponse?> GetAsync(int id);
        Task<int> CreateAsync(MailingProgramacionRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ZonaHorarioResponse>> GetZonaHorarioAsync();
        Task<IEnumerable<PeriodicidadResponse>> GetPeriodicidadAsync();
        Task<IEnumerable<TipoReporteResponse>> GetTipoReporteAsync(); 
    }
}
