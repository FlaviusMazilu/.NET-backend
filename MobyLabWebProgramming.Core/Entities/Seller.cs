using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.Entities;

public class Seller : BaseEntity
{
    public string Name { get; set; } = null!;
    public int Rating { get; set; }
    [Range(1, 5)]
    public int CUI { get; set; }
    
    public ICollection<Product> Products { get; set; } = null!;
}