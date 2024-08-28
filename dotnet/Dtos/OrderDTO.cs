namespace ecomercewebapi.Dtos
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ShipAddressId { get; set; }
        public long ShipperId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
    }

}
