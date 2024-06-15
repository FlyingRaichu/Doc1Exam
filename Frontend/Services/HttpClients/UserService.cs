using System.Text.Json;
using Database.Domain;
using Frontend.Services.HttpClients.Interfaces;

namespace Frontend.Services.HttpClients;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        var response = await _client.GetAsync("/Users");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var users = JsonSerializer.Deserialize<List<User>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return users;
    }
}