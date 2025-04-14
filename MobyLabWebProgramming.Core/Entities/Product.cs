namespace MobyLabWebProgramming.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    public string? PicturePath { get; set; }
    public float Rating { get; set; }
    public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
    public ICollection<ProductSeller> ProductSellers { get; set; } = [];
}

