using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class User
{
    [Key]
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int SecurityLevel { get; set; }

    public User()
    {
    }

    public User(string name, string password, string role, int securityLevel)
    {
        Name = name;
        Password = password;
        Role = role;
        SecurityLevel = securityLevel;
    }

    public User(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name + "\n" + Password + "\n" + Role + "\n" + SecurityLevel;
    }
}