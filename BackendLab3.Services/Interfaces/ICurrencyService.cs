using BackendLab3.Interfaces.Dto;
using BackendLab3.Models;
using BackendLab3.Services.Dto;
using BackendLab3.Services.Dto.Currency;

namespace BackendLab3.Services.Interfaces;

public interface ICurrencyService
{
    Task<IEnumerable<Currency>> GetAllAsync();
    Task<Currency?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCurrencyDto currency);
    Task UpdateAsync(int id, UpdateCurrencyDto currency);
    Task DeleteAsync(int id);
}