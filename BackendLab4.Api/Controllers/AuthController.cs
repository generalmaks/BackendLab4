using BackendLab3.Services.Dto.User;
using BackendLab3.Services.Exceptions;
using BackendLab3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendLab3.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController(IAuthService authService, IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GetLoginDto>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var answer = await authService.Login(loginDto.Username, loginDto.UnhashedPassword);
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

    [HttpPost]
    public async Task<ActionResult> Register([FromBody] CreateUserDto dto)
    {
        try
        {
            await userService.CreateAsync(dto);
            return Created();
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