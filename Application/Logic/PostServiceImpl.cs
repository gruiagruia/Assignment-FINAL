using Application.DAOs;
using Contracts;
using Entities.Models;

namespace Application.Logic;

public class PostServiceImpl : IPostService
{
    private IPostDAO _postDao;

    public PostServiceImpl(IPostDAO postDao)
    {
        _postDao = postDao;
    }

    public async Task<ICollection<Post>> GetAsync()
    {
        return await _postDao.GetAsync();
    }

    public async Task<Post> GetById(int id)
    {
        return await _postDao.GetById(id);
    }

    public async Task<Post> AddAsync(Post post)
    {
        return await _postDao.AddAsync(post);
    }

    public async Task DeleteAsync(int id)
    {
        await _postDao.DeleteAsync(id);
    }

    public async Task UpdateAsync(Post post)
    {
        await _postDao.UpdateAsync(post);
    }
}