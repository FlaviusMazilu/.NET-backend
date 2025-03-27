using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class CategoryService(IRepository<WebAppDatabaseContext> repository) : ICategoryService
{
    public async Task<ServiceResponse<CategoryDTO>> GetCategory(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new CategoryProjectionSpec(), cancellationToken);

        return result != null ?
            ServiceResponse.ForSuccess(result) :
            ServiceResponse.FromError<CategoryDTO>(CommonErrors.CategoryNotFound);

    }

    public async Task<ServiceResponse<List<CategoryDTO>>> GetCategories(CancellationToken cancellationToken = default)
    {
        var result = await repository.ListAsync(new CategoryProjectionSpec(), cancellationToken);
        
        // even if there is no Category registered yet, it will return an empty list
        return ServiceResponse.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetCategoryCount(CancellationToken cancellationToken = default)
    {
        var result = await repository.GetCountAsync<Category>(cancellationToken);
        return ServiceResponse.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddCategory(CategoryAddDTO category, UserDTO? requestingUser = null,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(CommonErrors.UnauthorizedAdminAction);
        
        var result = await repository.GetAsync(new CategorySpec(category.Name), cancellationToken);

        if (result != null)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Conflict, "The category already exists"));

        var newCategory = new Category()
        {
            Name = category.Name,
            Description = category.Description
        };

        await repository.AddAsync(newCategory, cancellationToken);
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateCategory(CategoryUpdateDTO category, UserDTO? requestingUser = null,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(CommonErrors.UnauthorizedAdminAction);
       
        var entity = await repository.GetAsync<Category>(category.id, cancellationToken);

        if (entity == null)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.NotFound, "Category not found"));
        
        entity.Name = category.Name ?? entity.Name;
        entity.Description = category.Description ?? entity.Description;
        
        await repository.UpdateAsync(entity, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCategory(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(CommonErrors.UnauthorizedAdminAction);

        var result = await repository.GetAsync(new CategorySpec(id), cancellationToken);
        if (result == null)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.NotFound, "Category not found"));
        
        await repository.DeleteAsync<Category>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }
}