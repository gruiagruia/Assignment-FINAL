using Entities.Models;

namespace Application.DAOs;

public interface IPostDAO
{
    public Task<ICollection<Post>> GetAsync();
    public Task<Post> GetById(int id);
    public Task<Post> AddAsync(Post todo);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(Post todo);
}