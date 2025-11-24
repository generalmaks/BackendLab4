using BackendLab3.DataAccess.Models;
using BackendLab3.Services.Dto.Expense;
using BackendLab3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendLab3.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpenseController(IExpensesService service) : ControllerBase
{
    // GET: api/expenses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Expense>>> List()
    {
        var expenses = await service.ListAsync();
        return Ok(expenses);
    }

    // GET: api/expenses/{id}?userId=1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Expense>> Get(int id, [FromQuery] int userId)
    {
        var expense = await service.GetByIdAsync(id, userId);
        if (expense is null)
            return NotFound("Expense not found.");

        return Ok(expense);
    }

    // POST: api/expenses?userId=1
    [HttpPost]
    public async Task<ActionResult> Create([FromQuery] int userId, [FromBody] CreateExpenseDto dto)
    {
        try
        {
            var expenseId = await service.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(Get), new { id = expenseId, userId = userId }, dto);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // PUT: api/expenses/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateExpenseDto dto)
    {
        try
        {
            await service.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // DELETE: api/expenses/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await service.DeleteAsync(id);
            return NoContent(); // 204
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}