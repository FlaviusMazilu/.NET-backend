using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.Entities;

public class Seller : BaseEntity
{
    public string Name { get; set; } = null!;
    public int Rating { get; set; }
    [Range(1, 5)]
    public int CUI { get; set; }
    public Guid UserAccountId { get; set; }
    
    public User UserAccount { get; set; } = null!;    
    public ICollection<Product> Products { get; set; } = null!;
}