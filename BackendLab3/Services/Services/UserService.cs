using BackendLab3.Context;
using BackendLab3.Interfaces.Services;
using BackendLab3.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendLab3.Services.Services;

public class UserService(AppDbContext context) : IUserService
{
    public async Task<Currency> GetDefaultCurrencyAsync(int userId)
    {
        var user = await context.Users
            .Include(user => user.DefaultCurrency)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            throw new Exception("User not found.");
        return user.DefaultCurrency;
    }

    public async Task SetDefaultCurrencyAsync(int userId, int currencyId)
    {
        var user = await context.Users
            .Include(user => user.DefaultCurrency)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            throw new Exception("User not found.");

        user.DefaultCurrencyId = currencyId;
        await context.SaveChangesAsync();
    }
}