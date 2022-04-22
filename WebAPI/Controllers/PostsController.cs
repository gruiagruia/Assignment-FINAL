using Application.DAOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private IPostDAO _postDao;

    public PostsController(IPostDAO postDao)
    {
        _postDao = postDao;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Post>>> GetAll()
    {
        try
        {
            ICollection<Post> posts = await _postDao.GetAsync();
            return Ok(posts);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> AddPost([FromBody] Post post)
    {
        try
        {
            Post added = await _postDao.AddAsync(post);
            return Created($"/posts/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("/posts/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<Post>> GetById([FromBody] int id)
    {
        try
        {
            ICollection<Post> posts = await _postDao.GetAsync();
            return posts.First(t => t.Id == id);

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}