namespace Farmacheck.Application.Models.Users;

public class RelUserByRoleResponse
{
    public int RoleByUserId { get; set; }
    public int RoleId { get; set; }
    public int UserId { get; set; }
    public string? Nombre { get; set; }
    public bool Estatus { get; set; }
    public int UnidadDeNegocioId { get; set; }
    public string? UnidadDeNegocioNombre { get; set; }
    public int TotalClientesAsignados { get; set; }
}
