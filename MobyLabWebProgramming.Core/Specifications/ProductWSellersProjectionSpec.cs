using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects.Product;
using MobyLabWebProgramming.Core.DataTransferObjects.Seller;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class ProductWSellersProjectionSpec : Specification<Product, ProductWSellersDTO>
{
    public ProductWSellersProjectionSpec(bool orderByCreatedAt = false) => Query.Select(e => new ProductWSellersDTO()
        {
            Id = e.Id,
            Name = e.Name,
            CategoryId = e.CategoryId,
            TotalQuantity = e.ProductSellers.Sum(p => (int?)p.Quantity) ?? 0,
            StartingPrice = e.ProductSellers.Min(p => (float?)p.Price) ?? 0f,
            Rating = e.Rating,
            Sellers = e.Sellers.Select(s => new SellerDTO
            {
                Id = s.Id,
                Name = s.Name,
                Rating = s.Rating,
                CUI = s.CUI,
            }).ToList()
        })
        .OrderByDescending(e => e.CreatedAt, orderByCreatedAt);
    
    public ProductWSellersProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);
}