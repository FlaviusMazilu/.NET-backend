using Microsoft.AspNetCore.Http;

namespace MobyLabWebProgramming.Core.DataTransferObjects.Product;

public class ProductImageAddDTO
{
    public IFormFile File { get; set; } = null!;
    public Guid productId { get; set; }
}