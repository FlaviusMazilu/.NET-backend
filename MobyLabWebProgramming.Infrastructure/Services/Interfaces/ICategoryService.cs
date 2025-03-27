using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICategoryService
{
    /// <summary>
    /// GetCategory will provide the information about a category given its Id.
    /// </summary>
    public Task<ServiceResponse<CategoryDTO>> GetCategory(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// GetCategories gets all categories from the database - accessible to anyone
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ServiceResponse<List<CategoryDTO>>> GetCategories(CancellationToken cancellationToken = default);

    /// <summary>
    /// GetCategoryCount returns the number of categories in the database - accessible to anyone
    /// </summary>
    public Task<ServiceResponse<int>> GetCategoryCount(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// AddCategory adds an category and verifies if requesting user has permissions to add one(only admins).
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddCategory(CategoryAddDTO category, UserDTO? requestingUser = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// UpdateUser updates an user and verifies if requesting user has permissions to update it, if the user is his own then that should be allowed.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateCategory(CategoryUpdateDTO category, UserDTO? requestingUser = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// DeleteCategory deletes a category and verifies if requesting user has permissions to delete it(only admins can perform this)
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteCategory(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default);
}