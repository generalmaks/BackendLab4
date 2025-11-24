using BackendLab3.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendLab3.Controllers;

[ApiController]
[Route("/healthcheck")]
public class HealthcheckController : ControllerBase
{
    [HttpGet]
    public ActionResult<Healthcheck> HealthCheck()
    {
        try
        {
            var status = new Healthcheck();
            return Ok(status);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}