using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Post
{
    [Key]
    public int Id { get; set; }
    public string Owner { get; set; }
    [Required,MaxLength(128)]
    public string Title { get; set; }
    [Required,MaxLength(5000)]
    public string Body { get; set; }

    public Post(string owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }

    public Post(int id)
    {
        Id = id;
    }

    public Post()
    {
        
    }
}