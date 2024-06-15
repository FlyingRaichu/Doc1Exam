using Database.Domain;
using DOCApiTier.Logic.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DOCApiTier.Logic.LogicInterfaces;

public interface IUserLogic
{
    Task<ICollection<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int userId);
    Task<User> CreateUserAsync(User user);
    Task UpdateUserAsync(UserUpdateDto dto);
    Task RemoveUserByIdAsync(int userId);
}