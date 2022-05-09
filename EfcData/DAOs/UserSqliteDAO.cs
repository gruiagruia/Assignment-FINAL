using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData;

public class UserSqliteDAO : IUserService
{
    private readonly UserContext context;

    public UserSqliteDAO(UserContext context)
    {
        this.context = context;
    }

    public async Task<User> GetUserAsync(string username)
    {
        User toFind = new User(username);
        return await context.Users.FindAsync(toFind);
    }

    public async Task AddUserAsync(User user)
    {
        await context.AddAsync(user);
        await context.SaveChangesAsync();
    }
}