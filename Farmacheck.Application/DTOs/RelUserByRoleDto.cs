namespace Farmacheck.Application.DTOs;

public class RelUserByRoleDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int RolId { get; set; }
    public string? RolNombre { get; set; }
    public bool Estatus { get; set; }
    public int UnidadDeNegocioId { get; set; }
    public string? UnidadDeNegocioNombre { get; set; }
    public int TotalClientesAsignados { get; set; }
}
