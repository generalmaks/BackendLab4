namespace BackendLab3.Interfaces.Dto;

public record CreateCurrencyDto
{
    public required int Code { get; set; }
    public required string Name { get; set; }
    public required string Symbol { get; set; }
}