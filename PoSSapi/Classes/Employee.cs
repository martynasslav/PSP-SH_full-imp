using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace PoSSapi.Classes;

public class Employee
{
	[Key]
	public string Id { get; set; }
	[Required]
	public string Username { get; set; }
	[Required]
	public string Password { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public DateTime Birthday { get; set; }
	public int MonthlyWorkHours { get; set; }
	public string Address { get; set; }
	[Required]
	public bool IsManager { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	[Required]
	[ForeignKey("Location")]
	public string LocationId { get; set; }
}
