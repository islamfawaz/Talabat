namespace Route.Talabat.Application.Abstraction.Order.Models
{
    public class OrderToCreateDto
    {
        public required string BasketId { get; set; }
        public required int DeliveryMethodId { get; set; }
        public required AddressDto ShipToAddress { get; set; }
    }
}
