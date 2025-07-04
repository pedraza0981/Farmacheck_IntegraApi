using Farmacheck.Infrastructure.Models.Brands;

namespace Farmacheck.Models.Request
{
    public class UpdateBrandRequest 
    {
        public int Id { get; set; }

        public bool? Estatus { get; set; }
    }
}
