namespace MobyLabWebProgramming.Core.DataTransferObjects.Product;

public class ProductAddDTO
{
    public Guid ProductId { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public float? Discount { get; set; }
}