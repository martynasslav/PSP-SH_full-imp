namespace Classes;

public class Shift
{
    public string Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public Employee Employee { get; set; }
    
    static public Shift GenerateRandom(string? id=null)
    {
            var shift = new Shift();
            shift.Id = id ?? Guid.NewGuid().ToString();
            shift.StartDate = DateTime.Now;
            shift.FinishDate = DateTime.Now;
            shift.CheckInDate = DateTime.Now;
            shift.CheckOutDate = DateTime.Now;
            shift.Employee = Employee.GenerateRandom();
            return shift;
    }
}
