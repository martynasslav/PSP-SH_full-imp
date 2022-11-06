namespace Classes;

public class Shift
{
    public string Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public Employee Employee { get; set; }
}
