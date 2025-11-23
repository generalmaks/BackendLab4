namespace BackendLab3.Services.Dto.User;

public record UpdateUserDto()
{
    public string? Username { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public int? DefaultCurrencyId { get; set; }
}