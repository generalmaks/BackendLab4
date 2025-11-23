namespace BackendLab3.Services.Dto.Expense;

public record CreateExpenseDto
{
    public required string Description { get; set; } = null!;
    public required decimal Amount { get; set; }
    public required DateTime Date { get; set; }
    public int CurrencyId { get; set; }
}