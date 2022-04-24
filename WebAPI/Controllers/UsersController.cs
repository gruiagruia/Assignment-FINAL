using Application.DAOs;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet,Route("{username}")]
    public async Task<ActionResult<User>> GetUser( string username)
    {
        try
        {
            User user = await _userService.GetUserAsync(username);
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody] User user)
    {
        try
        {
            await _userService.AddUserAsync(user);
            return Created($"/users/{user.Name}", user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}