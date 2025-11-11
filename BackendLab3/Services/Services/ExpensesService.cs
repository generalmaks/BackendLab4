using BackendLab3.Context;
using BackendLab3.Interfaces.Dto;
using BackendLab3.Interfaces.Services;
using BackendLab3.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendLab3.Services.Services;

public class ExpensesService(AppDbContext context) : IExpensesService
{
    public async Task<int> CreateAsync(int userId, CreateExpenseDto dto)
    {
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

    public async Task<Expense?> GetByIdAsync(int id, int userId)
    {
        return await context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
    }
}