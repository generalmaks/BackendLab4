using BackendLab3.Models;
using BackendLab3.Services.Dto.User;

namespace BackendLab3.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> ListUsers();
    Task<User> Get(int id);
    Task<int> CreateAsync(CreateUserDto dto);
    Task UpdateAsync(int id, UpdateUserDto dto);
    Task<Currency> GetDefaultCurrencyAsync(int userId);
    Task SetDefaultCurrencyAsync(int userId, int currencyId);
    Task DeleteAsync(int id);
}