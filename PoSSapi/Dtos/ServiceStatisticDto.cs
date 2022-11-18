namespace Dtos;

public record struct ServiceStatisticDto
{
    public string ServiceId { get; set; }
    public int TimesOrdered { get; set; }
}
