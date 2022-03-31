using Entities.Models;

namespace Contracts;

public interface IUserService
{
    public Task<User> GetUserAsync(string username);
    public Task AddUserAsync(string username, string password);
}