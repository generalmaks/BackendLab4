namespace BackendLab3.Services.Dto.User;

public record LoginDto
{
    public string Username { get; set; }
    public string UnhashedPassword { get; set; }
}