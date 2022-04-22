using System.Text;
using System.Text.Json;
using Application.DAOs;
using Entities.Models;

namespace HttpServices;

public class UserHttpClient : IUserDAO
{
    private IUserDAO _userDao;



    public async Task<User> GetUserAsync(string username)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        using HttpClient client = new(clientHandler);
        HttpResponseMessage response = await client.GetAsync($"https://localhost:7211/users/{username}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        User returned = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return returned;
        
    }

    public async Task<User> AddUserAsync(User user)
    {
        Console.WriteLine(user.ToString());
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        using HttpClient client = new(clientHandler);

        string userAsJson = JsonSerializer.Serialize(user);

        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("https://localhost:7211/users", content);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
    
        User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    
        return returned;
    }
}