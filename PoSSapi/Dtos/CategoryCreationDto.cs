using System.ComponentModel.DataAnnotations;

#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public class CategoryCreationDto
    {
        public string Name { get; set; }
        public string ClientId { get; set; }
    }
}
