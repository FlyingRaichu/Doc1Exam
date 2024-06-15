namespace DOCApiTier.Logic.DTOs;

public class UserUpdateDto
{
    public int Id { get; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }

    public UserUpdateDto(int id)
    {
        Id = id;
    }
}