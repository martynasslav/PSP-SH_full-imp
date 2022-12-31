#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public record CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public string CategoryId { get; set; }
        public string LocationId { get; set; }
    }
}
