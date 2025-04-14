namespace MobyLabWebProgramming.Core.DataTransferObjects.Product;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public int TotalQuantity { get; set; }
    public float StartingPrice { get; set; }
    public float Rating { get; set; }
}