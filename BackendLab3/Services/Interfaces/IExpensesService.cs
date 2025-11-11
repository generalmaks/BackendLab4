using BackendLab3.Interfaces.Dto;
using BackendLab3.Models;

namespace BackendLab3.Interfaces.Services;

public interface IExpensesService
{
    Task<int> CreateAsync(int userId, CreateExpenseDto dto);
    Task<Expense?> GetByIdAsync(int id, int userId);
}