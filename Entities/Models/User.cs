namespace Entities.Models;

public class User
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int SecurityLevel { get; set; }

    
    public User(string name, string password, string role, int securityLevel)
    {
        Name = name;
        Password = password;
        Role = role;
        SecurityLevel = securityLevel;
    }
}