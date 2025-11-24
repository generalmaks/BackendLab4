using System.Security.Cryptography;
using System.Text;
using BackendLab3.DataAccess.Context;
using BackendLab3.DataAccess.Models;
using BackendLab3.Services.Dto.User;
using BackendLab3.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackendLab3.Services.Services;

public class UserService(AppDbContext context) : IUserService
{
    public async Task<IEnumerable<User>> ListUsers()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User> Get(int id)
    {
        var found = await context.Users.FindAsync(id);
        return found ?? throw new KeyNotFoundException("User was not found");
    }

    public async Task<int> CreateAsync(CreateUserDto dto)
    {
        var existingUser =
            await context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username || u.Email == dto.Email);
        if (existingUser is not null)
            throw new KeyNotFoundException("User with respective username or email already exist.");

        if (await context.Currencies.FindAsync(dto.DefaultCurrencyId) is null)
            throw new KeyNotFoundException("Default currency not exist.");

        var hashedPassword = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(dto.UnhashedPassword))
        );
        
        var newUser = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            DefaultCurrencyId = dto.DefaultCurrencyId,
            HashedPassword = hashedPassword
        };

        await context.Users.AddAsync(newUser);
        return await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto)
    {
        var existingUser = await context.Users.FindAsync(id);
        if (existingUser is null)
            throw new KeyNotFoundException($"User with id {id} not found.");

        if (dto.Username is not null || dto.Email is not null)
        {
            var duplicateUser = await context.Users
                .Where(u => u.Id != id &&
                            (u.Username == dto.Username || u.Email == dto.Email))
                .FirstOrDefaultAsync();

            if (duplicateUser is not null)
                throw new("Another user with the same username or email already exists.");
        }

        if (dto.Username is not null)
            existingUser.Username = dto.Username;

        if (dto.Email is not null)
            existingUser.Email = dto.Email;

        if (dto.DefaultCurrencyId.HasValue)
            existingUser.DefaultCurrencyId = dto.DefaultCurrencyId.Value;

        await context.SaveChangesAsync();
    }

    public async Task<Currency> GetDefaultCurrencyAsync(int userId)
    {
        var user = await context.Users
            .Include(user => user.DefaultCurrency)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            throw new Exception("User not found.");
        return user.DefaultCurrency;
    }

    public async Task SetDefaultCurrencyAsync(int userId, int currencyId)
    {
        var user = await context.Users
            .Include(user => user.DefaultCurrency)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            throw new Exception("User not found.");

        user.DefaultCurrencyId = currencyId;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var found = await context.Users.FindAsync(id);
        if (found is null)
            throw new KeyNotFoundException("User not found.");
        context.Remove(found);
        await context.SaveChangesAsync();
    }
}