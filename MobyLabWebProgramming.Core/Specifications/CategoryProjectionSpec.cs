using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class CategoryProjectionSpec : Specification<Category, CategoryDTO>
{
    public CategoryProjectionSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new CategoryDTO()
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
        })
        .OrderByDescending(e => e.CreatedAt, orderByCreatedAt);

}