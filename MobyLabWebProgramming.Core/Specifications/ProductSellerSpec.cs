using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class ProductSellerSpec : Specification<ProductSeller>
{
    public ProductSellerSpec(Guid sellerId, Guid productId) => Query.Where(p => p.ProductId == productId && p.SellerId == sellerId);

    public ProductSellerSpec(Guid productId) => Query.Where(p => p.ProductId == productId);
}