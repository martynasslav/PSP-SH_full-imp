namespace Classes;

public class Employee
{
    public string Id { get; set; }
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
    public Location Location { get; set; }
    public List<Client> Clients { get; set; }
}
