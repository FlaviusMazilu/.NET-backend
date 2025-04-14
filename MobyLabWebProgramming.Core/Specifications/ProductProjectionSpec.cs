using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects.Product;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;

namespace MobyLabWebProgramming.Core.Specifications;

public class ProductProjectionSpec : Specification<Product, ProductDTO>
{
    public ProductProjectionSpec(bool orderByCreatedAt = false) => Query.Select(e => new ProductDTO()
    {
        Id = e.Id,
        Name = e.Name,
        CategoryId = e.CategoryId,
        TotalQuantity = e.ProductSellers.Sum(p => (int?)p.Quantity) ?? 0,
        StartingPrice = e.ProductSellers.Min(p => (float?)p.Price) ?? 0f,
        Rating = e.Rating,
    })
    .OrderByDescending(e => e.CreatedAt, orderByCreatedAt);
    
    public ProductProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);
    
    public ProductProjectionSpec(string? search) : this(true) // This constructor will call the first declared constructor with 'true' as the parameter. 
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); // This is an example on how database specific expressions can be used via C# expressions.
        // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}