using System.Text;
using System.Text.Json;
using Application.DAOs;
using Entities.Models;

namespace HttpServices;

public class PostHttpClient : IPostDAO
{
    public async Task<ICollection<Post>> GetAsync()
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        using HttpClient client = new(clientHandler);
       
        HttpResponseMessage response = await client.GetAsync("https://localhost:7211/posts");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public Task<Post> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Post> AddAsync(Post post)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        using HttpClient client = new(clientHandler);

        string todoAsJson = JsonSerializer.Serialize(post);

        StringContent content = new(todoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("https://localhost:7211/posts", content);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
    
        Post returned = JsonSerializer.Deserialize<Post>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    
        return returned;
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Post todo)
    {
        throw new NotImplementedException();
    }
}