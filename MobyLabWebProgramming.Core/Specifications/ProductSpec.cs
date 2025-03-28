using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class ProductSpec : Specification<Product>
{
    public ProductSpec(string name) => Query.Where(p => p.Name == name);
    
    public ProductSpec(Guid productId) => Query.Where(p => p.Id == productId);
}