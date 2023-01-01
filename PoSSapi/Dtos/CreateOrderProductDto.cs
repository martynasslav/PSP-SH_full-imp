#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public record CreateOrderProductDto
    {
        public string ProductId { get; set; }
        public float Quantity { get; set; }
        public string? Details { get; set; }
        public string OrderId { get; set; }
    }
}
