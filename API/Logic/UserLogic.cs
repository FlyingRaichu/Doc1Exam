using Database.Context;
using Database.Domain;
using Database.Factories;
using DOCApiTier.Logic.DTOs;
using DOCApiTier.Logic.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOCApiTier.Logic;

public class UserLogic : IUserLogic
{
    private readonly DatabaseContext _databaseContext;

    public UserLogic()
    {
        _databaseContext = new DatabaseContext(DatabaseFactory.BuildConnectionOptions());
    }

    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        return await _databaseContext.Users.ToListAsync();
    }
    
    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _databaseContext.Users.FindAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        return user;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        try
        {
            var entry = await _databaseContext.Users.AddAsync(user);
            await _databaseContext.SaveChangesAsync();
            var addedUser = entry.Entity;
       
            return addedUser;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateUserAsync(UserUpdateDto dto)
    {
        var existing = await GetUserByIdAsync(dto.Id);
        var usernameToUse = dto.Username ?? existing!.UserName;
        var passwordToUse = dto.Password ?? existing!.Password;
        var emailToUse = dto.Email ?? existing!.Email;

        User updated = new()
        {
            Id = existing!.Id,
            UserName = usernameToUse,
            Password = passwordToUse,
            Email = emailToUse
        };

        try
        {
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }

    public async Task RemoveUserByIdAsync(int userId)
    {
        try
        {
           var userToRemove = await GetUserByIdAsync(userId);
           _databaseContext.Users.Remove(userToRemove);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}