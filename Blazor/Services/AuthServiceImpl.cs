using System.Security.Claims;
using System.Text.Json;
using Blazor.Authentication;
using Contracts;
using Entities.Models;
using Microsoft.JSInterop;

namespace Blazor.Services;

public class AuthServiceImpl : IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    private readonly IUserService userService;
    private readonly IJSRuntime jsRuntime;

    public AuthServiceImpl(IUserService userService, IJSRuntime jsRuntime)
    {
        this.userService = userService;
        this.jsRuntime = jsRuntime;
    }

    public async Task LoginAsync(string username, string password)
    {
        User? user = await userService.GetUserAsync(username);

        ValidateLoginCredentials(password, user);

        await CacheUserAsync(user!);

        ClaimsPrincipal principal = CreateClaimsPrincipal(user);
        
        OnAuthStateChanged?.Invoke(principal);
    }

    public async Task RegisterAsync(User user)
    {
        await userService.AddUserAsync(user);
    }

    public async Task LogoutAsync()
    {
        await ClearUserFromCacheAsync();
        ClaimsPrincipal principal = CreateClaimsPrincipal(null);
        OnAuthStateChanged?.Invoke(principal);
    }

    public async Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = null;
        
        if (principal != null)
        {
            return principal;
        }

        string userAsJson = await jsRuntime.
            InvokeAsync<string>("sessionStorage.getItem", "currentUser");
    
        if (string.IsNullOrEmpty(userAsJson))
        {
            return new ClaimsPrincipal(new ClaimsIdentity());
        }
    
        User? user = JsonSerializer.Deserialize<User>(userAsJson);
    
        principal = CreateClaimsPrincipal(user);
        return principal;
    }

    private async Task<User?> GetUserFromCacheAsync()
    {
        string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        if (string.IsNullOrEmpty(userAsJson)) return null;
        User user = JsonSerializer.Deserialize<User>(userAsJson);
        return user;
    }

    private static void ValidateLoginCredentials(string password, User? user)
    {
        if (user == null)
        {
            throw new Exception("Username not found");
        }

        if (!string.Equals(password, user.Password))
        {
            throw new Exception("Password incorrect");
        }
    }

    private static ClaimsPrincipal CreateClaimsPrincipal(User? user)
    {
        if (user != null)
        {
            ClaimsIdentity identity = ConvertUserToClaimsIdentity(user);
            return new ClaimsPrincipal(identity);
        }

        return new ClaimsPrincipal();
    }

    private async Task CacheUserAsync(User user)
    {
        string serialisedData = JsonSerializer.Serialize(user);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
    }

    private async Task ClearUserFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }

    private static ClaimsIdentity ConvertUserToClaimsIdentity(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("Role", user.Role),
            new Claim("SecurityLevel", user.SecurityLevel.ToString())
        };

        return new ClaimsIdentity(claims, "apiauth_type");
    }
}