using System.ComponentModel.DataAnnotations;

#pragma warning disable 8618

namespace PoSSapi.Dtos
{
	public record LocationCreationDto
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Address { get; set; }
	}
}
