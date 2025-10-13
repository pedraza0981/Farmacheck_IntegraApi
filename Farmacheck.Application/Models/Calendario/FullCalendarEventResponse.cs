namespace Farmacheck.Application.Models.Calendario
{
    public class FullCalendarEventResponse
    {
        public string Id { get; set; } = default!;

        public string Title { get; set; } = default!;

        public DateTimeOffset? Start { get; set; }

        public DateTimeOffset? End { get; set; }

        public bool AllDay { get; set; }

        public string? BackgroundColor { get; set; }

        public string? BorderColor { get; set; }

        public string TypeCode { get; set; } = default!; // EVENTO | TAREA | CUESTIONARIO

        public string RecursoId { get; set; }

        public int CalendarioId { get; set; }

        public byte? Estatus { get; set; }

        public byte? Visibilidad { get; set; }

        public string? Ubicacion { get; set; }

        public string? Descripcion { get; set; }
    }
}
