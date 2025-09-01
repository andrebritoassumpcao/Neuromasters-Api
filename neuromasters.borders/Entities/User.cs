using Microsoft.AspNetCore.Identity;
namespace neuromasters.borders.Entities;
public class User : IdentityUser
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FullName { get; private set; }
    public string Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User() { }
    public User(string email, string fullName, string role)
    {
        Id = Guid.NewGuid();
        Email = email;
        FullName = fullName;
        Role = role;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}
