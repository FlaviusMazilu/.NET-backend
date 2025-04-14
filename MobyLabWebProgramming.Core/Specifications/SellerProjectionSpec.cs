using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects.Seller;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class SellerProjectionSpec : Specification<Seller, SellerDTO>
{
    public SellerProjectionSpec(bool orderByDescending = false) => Query.Select(e => new SellerDTO()
    {
        Id = e.Id,
        Name = e.Name,
        Rating = e.Rating,
        CUI = e.CUI,
    }).OrderByDescending(e => e.CreatedAt, orderByDescending);

    public SellerProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);
    
    public SellerProjectionSpec(string? search, Guid? productId) : this(true) // This constructor will call the first declared constructor with 'true' as the parameter. 
    {
        if (productId != null)
            Query.Where(s => s.Products.Any(p => p.Id == productId));
        
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