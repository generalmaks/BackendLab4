using BackendLab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendLab3.Controllers;

[ApiController]
[Route("/healthcheck")]
public class HealthcheckController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Healthcheck>> HealthCheck()
    {
        try
        {
            var status = await HealthCheck();
            return Ok(status);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}