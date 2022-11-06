namespace Classes;

public class Discount
{
    public string Id { get; set; }
    public DiscountType Type { get; set; }
    public decimal Amount { get; set; }
    public DiscountTargetType TargetType { get; set; }
    public string TargetId { get; set; }
}
