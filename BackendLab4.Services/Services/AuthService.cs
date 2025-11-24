using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackendLab3.DataAccess.Context;
using BackendLab3.DataAccess.Models;
using BackendLab3.Services.Dto.User;
using BackendLab3.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackendLab3.Services.Services;

public class AuthService(AppDbContext context, IConfiguration config) : IAuthService
{
    public async Task<GetLoginDto> Login(string username, string unhashedPassword)
    {
        var foundUser = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        
        if (foundUser is null)
            throw new InvalidCredentialException("Invalid credentials.");
        
        var hashedPassword = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(unhashedPassword)));

        if (hashedPassword != foundUser.HashedPassword)
            throw new InvalidCredentialException("Invalid credentials.");

        var claims = new List<Claim> { new(ClaimTypes.Name, username) };

        var jwt = GenerateToken(foundUser);

        var loginDto = new GetLoginDto
        {
            Token = jwt
        };

        return loginDto;
    }

    private string GenerateToken(User user)
    {
        var secret = config["Jwt:Key"]!;
        var issuer = config["Jwt:Issuer"]!;
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
        };

        var token = new JwtSecurityToken(
            issuer,
            issuer,
            claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}