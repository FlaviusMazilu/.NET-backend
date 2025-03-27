using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.Entities;

public class ProductSeller : BaseEntity
{
    public Guid SellerId { get; set; }
    public Guid ProductId { get; set; }
    
    [Range(0f, 1f)]
    public int Price { get; set; }
    public float Discount { get; set; }
    public int Quantity { get; set; }
}