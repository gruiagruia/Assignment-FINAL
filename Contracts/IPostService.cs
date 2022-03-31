using Entities.Models;

namespace Contracts;

public interface IPostService
{
    public Task<ICollection<Post>> GetAsync();
    public Task<Post> GetById(int id);
    public Task<Post> AddAsync(Post post);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(Post post);
}