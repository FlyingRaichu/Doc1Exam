using Database.Domain;
using DOCApiTier.Logic.DTOs;
using DOCApiTier.Logic.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DOCApiTier.Controllers;


//Users controller
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic _logic;

    public UsersController(IUserLogic logic)
    {
        _logic = logic;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<User>>> GetAllUsersAsync()
    {
        try
        {
            var users = await _logic.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromRoute] int id)
    {
        try
        {
            var user = await _logic.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUserAsync([FromBody] User user)
    {
        try
        {
            var created = await _logic.CreateUserAsync(user);
            return Created($"/users/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUserAsync(UserUpdateDto dto)
    {
        try
        {
            await _logic.UpdateUserAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveUserByIdAsync(int userId)
    {
        try
        {
            await _logic.RemoveUserByIdAsync(userId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}