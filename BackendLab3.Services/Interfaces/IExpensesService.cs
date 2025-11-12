using BackendLab3.Models;
using BackendLab3.Services.Dto.Expense;

namespace BackendLab3.Services.Interfaces;

public interface IExpensesService
{
    Task<IEnumerable<Expense>> ListAsync();
    Task<Expense?> GetByIdAsync(int id, int userId);
    Task<int> CreateAsync(int userId, CreateExpenseDto dto);
    Task UpdateAsync(int id, UpdateExpenseDto dto);
    Task DeleteAsync(int id);
}