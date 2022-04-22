using Application.DAOs;
using Entities.Models;

namespace FileData.DataAccess;

public class UserFileDAO : IUserDAO
{
    private JsonContext _jsonContext;

    public UserFileDAO(JsonContext jsonContext)
    {
        _jsonContext = jsonContext;
    }

    public async Task<User> GetUserAsync(string username)
    {
        User user =  _jsonContext.Forum.Users.First((user => user.Name == username));
        return user;
    }

    public async Task<User> AddUserAsync(User user)
    { 
        _jsonContext.Forum.Users.Add(user);
        await _jsonContext.SaveChangesAsync();
       return user;
    }
}