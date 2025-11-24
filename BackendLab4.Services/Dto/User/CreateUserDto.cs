namespace BackendLab3.Services.Dto.User;

public record CreateUserDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int DefaultCurrencyId { get; set; }
    public string UnhashedPassword { get; set; } = null!;
}