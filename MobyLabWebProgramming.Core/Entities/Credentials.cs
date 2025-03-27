namespace MobyLabWebProgramming.Core.Entities;

public class Credentials : BaseEntity
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public User User { get; set; } = null!;
}