#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public record CreateServiceDto
    {
        public string Name { get; set; }
        public string? EmployeeId { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string CategoryId { get; set; }
        public string LocationId { get; set; }
    }
}
