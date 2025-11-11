using BackendLab3.Models;

namespace BackendLab3.Interfaces.Services;

public interface ICurrencyService
{
    Task<IEnumerable<Currency>> GetAllAsync();
    Task<Currency?> GetByIdAsync(int id);
}