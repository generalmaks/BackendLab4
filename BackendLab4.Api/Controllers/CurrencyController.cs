using BackendLab3.DataAccess.Models;
using BackendLab3.Services.Dto.Currency;
using BackendLab3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UpdateCurrencyDto = BackendLab3.Services.Dto.Currency.UpdateCurrencyDto;

namespace BackendLab3.Controllers;

[ApiController]
[Route("api/currency")]
public class CurrencyController(ICurrencyService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Currency>>> List()
    {
        try
        {
            var list = await service.GetAllAsync();
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Currency>> Get(int id)
    {
        try
        {
            var currency = await service.GetByIdAsync(id);
            return Ok(currency);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateCurrencyDto dto)
    {
        try
        {
            await service.CreateAsync(dto);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateCurrencyDto dto)
    {
        try
        {
            await service.UpdateAsync(id, dto);
            return NoContent();

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}