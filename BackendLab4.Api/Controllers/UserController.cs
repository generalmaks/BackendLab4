using BackendLab3.DataAccess.Models;
using BackendLab3.Services.Dto.User;
using BackendLab3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendLab3.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> List()
    {
        var users = await _service.ListUsers();
        return Ok(users);
    }
    
    // GET: api/user/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<int>> Get(int id)
    {
        try
        {
            var user = await _service.Get(id);
            return Ok(user);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // GET: api/users/{id}/default-currency
    [HttpGet("{id:int}/default-currency")]
    public async Task<ActionResult<Currency>> GetDefaultCurrency(int id)
    {
        try
        {
            var currency = await _service.GetDefaultCurrencyAsync(id);
            return Ok(currency);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateUserDto dto)
    {
        try
        {
            var createdId = await _service.CreateAsync(dto);
            return CreatedAtAction(
                nameof(Get),
                new { id = createdId },
                new { id = createdId }
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // PUT: api/users/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        try
        {
            await _service.UpdateAsync(id, dto);
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

    // PUT: api/users/{id}/default-currency
    [HttpPut("{id:int}/default-currency")]
    public async Task<ActionResult> SetDefaultCurrency(int id, [FromQuery] int currencyId)
    {
        try
        {
            await _service.SetDefaultCurrencyAsync(id, currencyId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
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
}