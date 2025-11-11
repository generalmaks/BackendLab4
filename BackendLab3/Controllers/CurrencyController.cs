using BackendLab3.Interfaces.Services;
using BackendLab3.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
}