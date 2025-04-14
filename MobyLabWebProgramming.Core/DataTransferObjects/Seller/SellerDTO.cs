namespace MobyLabWebProgramming.Core.DataTransferObjects.Seller;

public class SellerDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Rating { get; set; }
    public int CUI { get; set; }
}