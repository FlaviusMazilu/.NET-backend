using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class CategorySpec : Specification<Category>
{
    public CategorySpec(Guid guid) => Query.Where(e => e.Id == guid);
    
    public CategorySpec(string name) => Query.Where(e => e.Name == name);
    
}