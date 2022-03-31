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
        return  _jsonContext.Forum.Users.First((user => user.Name == username));
    }

    public void AddUserAsync(string username, string password)
    {
        _jsonContext.Forum.Users.Add(new User(username,password,"user",5));
    }
}