using Entities.Models;

namespace Application.DAOs;

public interface IUserDAO
{
    public Task<User> GetUserAsync(string username);
    public void AddUserAsync(string username, string password);
}