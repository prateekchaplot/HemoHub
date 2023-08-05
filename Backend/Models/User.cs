namespace Backend.Models;

public class User
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}