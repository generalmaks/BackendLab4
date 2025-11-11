namespace BackendLab3.Interfaces.Dto;

public class CreateExpenseDto
{
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int? CurrencyId { get; set; }
}