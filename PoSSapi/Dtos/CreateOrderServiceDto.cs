#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public record CreateOrderServiceDto
    {
        public string ServiceId { get; set; }
        public float Quantity { get; set; }
        public string? Details { get; set; }
        public string OrderId { get; set; }
    }
}
