namespace MobyLabWebProgramming.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
    public ICollection<ProductSeller> ProductSellers { get; set; } = [];
}

