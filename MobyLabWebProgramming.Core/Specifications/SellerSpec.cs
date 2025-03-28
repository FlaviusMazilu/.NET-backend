using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class SellerSpec : Specification<Seller>
{
    public SellerSpec(int CUI) => Query.Where(e => e.CUI == CUI);
    
    public SellerSpec(Guid id, bool includeUser = false)
    {
        if (includeUser)
            Query.Where(e => e.Id == id).Include(e => e.UserAccount);
        else
            Query.Where(e => e.Id == id);
    }
}