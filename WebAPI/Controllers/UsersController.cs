using Application.DAOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserDAO _userDao;

    public UsersController(IUserDAO userDao)
    {
        _userDao = userDao;
    }

    [HttpGet,Route("{username}")]
    public async Task<ActionResult<User>> GetUser( string username)
    {
        try
        {
            User user = await _userDao.GetUserAsync(username);
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
            await _userDao.AddUserAsync(user);
            return Created($"/users/{user.Name}", user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}