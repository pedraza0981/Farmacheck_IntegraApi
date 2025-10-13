using Farmacheck.Application.Models.Calendario;

namespace Farmacheck.Application.Interfaces
{
    public interface ICalendarioClient
    {
        Task<IEnumerable<FullCalendarEventResponse>> GetNonRecurringAsync(int usuarioId, int anio);

        Task<IEnumerable<FullCalendarEventResponse>> GetNonRecurringAsync(int usuarioId, int anio, int mes);

        Task<AddEventoCalendarResponse> AddEventoAsync(AddEventoCalendarRequest request);

        Task<AddArchivoCalendarResponse> AddArchivoAsync(AddArchivoCalendarRequest request);

        Task<ConfiguracionCalendarioResponse> GetConfig(int usuarioId, int calendarioId);

        Task<bool> PutConfig(ConfiguracionCalendarioResponse request);

    }
}
