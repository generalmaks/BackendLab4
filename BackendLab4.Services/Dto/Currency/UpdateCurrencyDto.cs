namespace BackendLab3.Services.Dto.Currency;

public record UpdateCurrencyDto
{
    public required int? Code { get; set; }
    public required string? Name { get; set; }
    public required string? Symbol { get; set; }
}