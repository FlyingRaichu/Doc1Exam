using Database.Domain;

namespace Frontend.Services.HttpClients.Interfaces;

public interface IUserService
{
    Task<ICollection<User>> GetAllUsersAsync();
}