using System.Security.Claims;
using Entities.Models;

namespace Blazor.Authentication;

public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task RegisterAsync(User user);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}