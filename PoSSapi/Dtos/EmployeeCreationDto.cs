using System.ComponentModel.DataAnnotations;

#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public record EmployeeCreationDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public int MonthlyWorkHours { get; set; }
        public string Address { get; set; }
        public bool IsManager { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LocationId { get; set; }
	}
}
