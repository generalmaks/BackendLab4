using BackendLab3.Context;
using BackendLab3.Interfaces.Services;
using BackendLab3.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendLab3.Services.Services;

public class CurrencyService(AppDbContext context) : ICurrencyService
{
    public async Task<IEnumerable<Currency>> GetAllAsync()
    {
        return await context.Currencies.ToListAsync();
    }

    public async Task<Currency?> GetByIdAsync(int id)
    {
        return await context.Currencies.FirstOrDefaultAsync(c => c.Id == id);
    }
}