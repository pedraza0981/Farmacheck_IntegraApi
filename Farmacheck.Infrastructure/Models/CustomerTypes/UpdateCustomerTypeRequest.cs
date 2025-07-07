namespace Farmacheck.Infrastructure.Models.CustomerTypes
{
    public class UpdateCustomerTypeRequest : CustomerTypeRequest
    {
        public short Id { get; set; }
        public bool? Estatus { get; set; }
    }
}
