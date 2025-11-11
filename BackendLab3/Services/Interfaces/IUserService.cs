using BackendLab3.Models;

namespace BackendLab3.Interfaces.Services;

public interface IUserService
{
    Task<Currency> GetDefaultCurrencyAsync(int userId);
    Task SetDefaultCurrencyAsync(int userId, int currencyId);
}