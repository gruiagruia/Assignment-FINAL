using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcData;

public class PostContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source = D:\DNP\Assignment\EfcData\Post.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
    }
    
    public void Seed()
    {
        if (Posts.Any()) return;

        Post[] ts =
        {
            new Post("Darius","Ce faci?", "Bn u."),
            new Post("Darius","Ce faci?", "Bn u."),
            new Post("Darius","Ce faci?", "Bn u."),
            new Post("Darius","Ce faci?", "Bn u.")
            
        };
        Posts.AddRange(ts);
        SaveChanges();
    }
}