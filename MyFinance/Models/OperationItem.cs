namespace MyFinance.Models;

public class OperationItem : BaseModel
{
    public string Icon { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string Color { get; set; }
}
