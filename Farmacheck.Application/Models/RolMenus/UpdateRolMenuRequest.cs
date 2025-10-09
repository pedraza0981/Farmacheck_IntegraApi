namespace Farmacheck.Application.Models.RolMenus
{
    public class UpdateRolMenuRequest
    {
        public int Id { get; set; }

        public int RolId { get; set; }

        public int MenuId { get; set; }

        public bool PuedeVer { get; set; } = true;
    }
}
