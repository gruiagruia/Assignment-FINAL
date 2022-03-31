namespace Entities.Models;

public class ForumContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Post> Posts { get; set; }

    public ForumContainer()
    {
        Users = new List<User>();
        Posts = new List<Post>();
    }
}