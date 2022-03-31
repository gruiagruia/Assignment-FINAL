using Application.DAOs;
using Contracts;
using Entities.Models;

namespace Application.Logic;

public class UserServiceImpl : IUserService
{
    private IUserDAO _userDao;

    public UserServiceImpl(IUserDAO userDao)
    {
        _userDao = userDao;
    }

    public async Task<User> GetUserAsync(string username)
    {
        return await _userDao.GetUserAsync(username);
    }

    public async Task AddUserAsync(string username, string password)
    {
        _userDao.AddUserAsync(username, password);
    }
}