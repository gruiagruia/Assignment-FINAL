using Contracts;
using Entities.Models;

namespace Application.DAOs;

public interface IUserDAO 
{
    public Task<User> GetUserAsync(string username);
    public Task<User> AddUserAsync(User user);
}