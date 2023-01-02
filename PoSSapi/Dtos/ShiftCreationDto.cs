using System.ComponentModel.DataAnnotations;

#pragma warning disable 8618

namespace PoSSapi.Dtos
{
	public record ShiftCreationDto
	{
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime FinishDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        [Required]
        public string EmployeeId { get; set; }
    }
}
