using BackendLab3.DataAccess.Context;
using BackendLab3.DataAccess.Models;
using BackendLab3.Services.Dto.Currency;
using BackendLab3.Services.Interfaces;
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

    public async Task<int> CreateAsync(CreateCurrencyDto dto)
    {
        var currency = new Currency
        {
            Code = dto.Code,
            Name = dto.Name,
            Symbol = dto.Symbol
        };
        await context.Currencies.AddAsync(currency);
        return await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateCurrencyDto dto)
    {
        var existing = await context.Currencies.FindAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Currency with id {id} not found");

        existing.Code = dto.Code ?? existing.Code;
        existing.Name = dto.Name ?? existing.Name;
        existing.Symbol = dto.Symbol ?? existing.Symbol;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var found = await context.Currencies.FindAsync(id);
        if (found is null)
            throw new KeyNotFoundException($"Currency with id {id} not found");
        context.Currencies.Remove(found);
        await context.SaveChangesAsync();
    }
}