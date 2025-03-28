namespace MobyLabWebProgramming.Core.DataTransferObjects.Product;

public class ProductCreateDTO
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public string Description { get; set; }
    
}