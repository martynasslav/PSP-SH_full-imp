using Enums;

#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public record CreateProductOrderDto
    {
        public OrderStatusState OrderStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public string Payments { get; set; }
        public int TableNumber { get; set; }
        public decimal Tips { get; set; }
        public ProductOrderType OrderType { get; set; }
    }
}
