using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Farmacheck.Application.Models.Calendario
{
    public class AddArchivoCalendarRequest
    {
        [Required] public IFormFile File { get; set; } = default!;

        public string NombreOriginal { get; set; } = default!;

        public string Extension { get; set; } = default!;

        public string MimeType { get; set; } = default!;

        public long TamanoBytes { get; set; }

        public string? HashSha256Base64 { get; set; } // opcional

        public byte Proveedor { get; set; }

        public string RutaAlmacen { get; set; } = default!;

        public int SubidoPorUsuarioId { get; set; }

        public bool EsPrivado { get; set; } = false;
    }
}
