using BackendLab3.DataAccess.Context;
using BackendLab3.DataAccess.Models;
using BackendLab3.Services.Dto.Expense;
using BackendLab3.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackendLab3.Services.Services;

public class ExpensesService(AppDbContext context) : IExpensesService
{
    public async Task<IEnumerable<Expense>> ListAsync()
    {
        return await context.Expenses.ToListAsync();
    }

    public async Task<Expense?> GetByIdAsync(int id, int userId)
    {
        return await context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
    }

    public async Task<int> CreateAsync(int userId, CreateExpenseDto dto)
    {
        if (await context.Users.FindAsync(userId) is null)
            throw new KeyNotFoundException("User was not found.");
        if (await context.Currencies.FindAsync(dto.CurrencyId) is null)
            throw new KeyNotFoundException("Currency was not found.");
        
        var expense = new Expense()
        {
            Description = dto.Description,
            Amount = dto.Amount,
            Date = dto.Date,
            UserId = userId,
            CurrencyId = dto.CurrencyId
        };

        await context.Expenses.AddAsync(expense);
        var expenseId = await context.SaveChangesAsync();
        return expenseId;
    }

    public async Task UpdateAsync(int id, UpdateExpenseDto dto)
    {
        var existing = await context.Expenses.FindAsync(id);
        if (existing is null)
            throw new KeyNotFoundException("Expense not found");

        existing.Description = dto.Description ?? existing.Description;
        existing.Amount = dto.Amount ?? existing.Amount;
        existing.Date = dto.Date ?? existing.Date;
        existing.CurrencyId = dto.CurrencyId ?? existing.CurrencyId;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var found = await context.Expenses.FindAsync(id);
        if (found is null)
            throw new KeyNotFoundException("Expense not found.");
        context.Expenses.Remove(found);
        await context.SaveChangesAsync();
    }
}