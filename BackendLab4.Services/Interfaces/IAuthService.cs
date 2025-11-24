using BackendLab3.Services.Dto.User;

namespace BackendLab3.Services.Interfaces;

public interface IAuthService
{
    Task<GetLoginDto> Login(string username, string unhashedPassword);
}