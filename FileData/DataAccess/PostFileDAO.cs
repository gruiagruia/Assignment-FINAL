using Application.DAOs;
using Entities.Models;

namespace FileData.DataAccess;

public class PostFileDAO : IPostDAO
{
    private JsonContext jsonContext;

    public PostFileDAO(JsonContext jsonContext)
    {
        this.jsonContext = jsonContext;
    }
    
    public async Task<ICollection<Post>> GetAsync()
    {
        ICollection<Post> posts = jsonContext.Forum.Posts;
        return posts;
    }
    
    public async Task<Post> GetById(int id)
    {
        return jsonContext.Forum.Posts.First(t => t.Id == id);
    }
    
    public async Task<Post> AddAsync(Post post)
    {
        if (!jsonContext.Forum.Posts.Any())
        {
            post.Id = 1;
        }
        else
        {
            int largestId = jsonContext.Forum.Posts.Max(t => t.Id);
            post.Id = largestId + 1;

        }
        
        jsonContext.Forum.Posts.Add(post);
        await jsonContext.SaveChangesAsync();
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        Post toDelete = jsonContext.Forum.Posts.First(t => t.Id == id);
        jsonContext.Forum.Posts.Remove(toDelete);
        await jsonContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post post)
    {
        Post toUpdate = jsonContext.Forum.Posts.First(t => t.Id == post.Id);
        toUpdate.Owner = post.Owner;
        await jsonContext.SaveChangesAsync();
    }
}