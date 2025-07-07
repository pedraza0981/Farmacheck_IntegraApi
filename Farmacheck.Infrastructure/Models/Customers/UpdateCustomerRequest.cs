namespace Farmacheck.Infrastructure.Models.Customers
{
    public class UpdateCustomerRequest : CustomerRequest
    {
        public int Id { get; set; }
        public bool? Estatus { get; set; }
    }
}
