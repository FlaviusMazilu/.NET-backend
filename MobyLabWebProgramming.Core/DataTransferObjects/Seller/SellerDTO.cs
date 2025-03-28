namespace MobyLabWebProgramming.Core.DataTransferObjects.Seller;

public class SellerDTO
{
    public string Name { get; set; } = null!;
    public int Rating { get; set; }
    public int CUI { get; set; }
    public ICollection<Entities.Product> Products { get; set; } = null!;
}