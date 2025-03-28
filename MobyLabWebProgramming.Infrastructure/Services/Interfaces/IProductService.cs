using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.Product;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProductService
{
    /// <summary>
    /// GetProduct will provide the information about a user given its product Id.
    /// </summary>
    public Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// GetProducts returns page with product information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<ProductDTO>>> GetProducts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// AddProduct is called by a Seller and registers themselves as selling this product with its own price, quantity, discount
    /// If the requesting user is null then no verification is performed as it indicates that to the application.
    /// </summary>
    public Task<ServiceResponse> AddProduct(ProductAddDTO product, UserDTO requestingUser, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// GetProductCount returns the number of products in the database.
    /// </summary>
    public Task<ServiceResponse<int>> GetProductsCount(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// UpdateProduct updates an user and verifies if requesting user has permissions to update it, if the user is his own then that should be allowed.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateProduct(ProductUpdateDTO product, UserDTO requestingUser, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// UpdateProductBySeller updates the inventory of a Seller of the productId specified
    /// </summary>
    public Task<ServiceResponse> UpdateProductBySeller(ProductUpdateBySellerDTO product, UserDTO requestingUser, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteProduct deletes a product and verifies if requesting user has permissions to delete it, if the user is his own then that should be allowed.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteProduct(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);

    /// <summary>
    /// DeleteProduct deletes a product sold by seller 'registeringUser'
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteProductBySeller(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);

    /// <summary>
    /// CreateProduct creates a new type of Product, and a Seller would be able to register as selling this product
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> CreateProduct(ProductCreateDTO product, UserDTO requestingUser,
        CancellationToken cancellationToken = default);
}