using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects.Seller;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class SellerProjectionSpec : Specification<Seller, SellerDTO>
{
    public SellerProjectionSpec(bool orderByDescending = false) => Query.Select(e => new SellerDTO()
    {
        Name = e.Name,
        Rating = e.Rating,
        CUI = e.CUI,
        Products = e.Products
    }).OrderByDescending(e => e.CreatedAt, orderByDescending);

    public SellerProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);
}