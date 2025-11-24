using BackendLab3.Services.Dto.User;
using BackendLab3.Services.Exceptions;
using BackendLab3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendLab3.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GetLoginDto>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var answer = await service.Login(loginDto.Username, loginDto.UnhashedPassword);
            return Ok(answer);
        }
        catch (InvalidCredentialsException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}