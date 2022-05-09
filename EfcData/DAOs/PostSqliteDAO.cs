using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData;

public class PostSqliteDAO : IPostService
{
    private readonly PostContext context;

    public PostSqliteDAO(PostContext context)
    {
        this.context = context;
    }

    public async Task<ICollection<Post>> GetAsync()
    {
        ICollection<Post> posts = await context.Posts.ToListAsync();
        return posts;
    }

    public async Task<Post> GetById(int id)
    {
        Post toFind = new Post(id);
        return await context.Posts.FindAsync(toFind);
    }

    public async Task<Post> AddAsync(Post post)
    {
        EntityEntry<Post> added = await context.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await context.Posts.FindAsync(id);
        if (existing is null)
        {
            throw new Exception($"Could not find Post with id {id}. Nothing was deleted");
        }

        context.Posts.Remove(existing);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(Post post)
    {
        context.Posts.Update(post);
        return context.SaveChangesAsync();
    }
}