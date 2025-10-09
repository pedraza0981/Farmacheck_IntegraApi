namespace Farmacheck.Application.Models.RolMenus
{
    public class RolMenuRequest
    {
        public int RolId { get; set; }

        public int MenuId { get; set; }

        public bool PuedeVer { get; set; } = true;
    }
}
