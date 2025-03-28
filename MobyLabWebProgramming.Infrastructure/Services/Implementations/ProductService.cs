using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.Product;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Implementation;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ProductService(IRepository<WebAppDatabaseContext> repository) : IProductService
{
    public async Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new ProductProjectionSpec(id), cancellationToken);
        
        return result != null ?
            ServiceResponse.ForSuccess(result) :
            ServiceResponse.FromError<ProductDTO>(new ErrorMessage(HttpStatusCode.NotFound, "Product not found.", ErrorCodes.EntityNotFound));
    }

    public async Task<ServiceResponse<PagedResponse<ProductDTO>>> GetProducts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await repository.PageAsync(pagination, new ProductProjectionSpec(pagination.Search), cancellationToken);
        
        return ServiceResponse.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddProduct(ProductAddDTO product, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Seller)
            return ServiceResponse.FromError(CommonErrors.GenericUnauthorizedAction);

        var result = await repository.GetAsync(new ProductSpec(product.ProductId), cancellationToken);
        if (result == null)
            return ServiceResponse.FromError(CommonErrors.ProductNotFound);
        
        var productSellers = await repository.GetAsync(new ProductSellerSpec(requestingUser.Id, product.ProductId), cancellationToken);
        if (productSellers != null)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Conflict, "Product already registered, use PUT to update"));

        var productSeller = new ProductSeller()
        {
            ProductId = product.ProductId,
            SellerId = requestingUser.Id,
            Price = product.Price,
            Discount = product.Discount ?? 0,
            Quantity = product.Quantity
        };
        
        await repository.AddAsync(productSeller, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> CreateProduct(ProductCreateDTO product, UserDTO requestingUser,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Seller && requestingUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(CommonErrors.GenericUnauthorizedAction);

        var result = await repository.GetAsync(new ProductSpec(product.Name), cancellationToken);
        if (result != null)
            return ServiceResponse.FromError(CommonErrors.EntityNameAlreadyExists);
        
        var resultCategory = await repository.GetAsync(new CategorySpec(product.CategoryId), cancellationToken);
        if (resultCategory == null)
            return ServiceResponse.FromError(CommonErrors.CategoryNotFound);
        
        var newProduct = new Product()
        {
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.CategoryId,
        };
        await repository.AddAsync(newProduct, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<int>> GetProductsCount(CancellationToken cancellationToken = default)
    {
        var result = await repository.GetCountAsync<Product>(cancellationToken);
        return ServiceResponse.ForSuccess(result);
    }

    public async Task<ServiceResponse> UpdateProduct(ProductUpdateDTO product, UserDTO requestingUser,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(CommonErrors.GenericUnauthorizedAction);
        
        var result = await repository.GetAsync(new ProductSpec(product.Id), cancellationToken);
        if (result == null)
            return ServiceResponse.FromError(CommonErrors.ProductNotFound);
        
        result.Name = product.Name ?? result.Name;
        result.CategoryId = product.CategoryId ?? result.CategoryId;
        result.Description = product.Description ?? result.Description;
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateProductBySeller(ProductUpdateBySellerDTO product, UserDTO requestingUser,
        CancellationToken cancellationToken)
    {
        if (requestingUser.Role != UserRoleEnum.Seller)
            return ServiceResponse.FromError(CommonErrors.GenericUnauthorizedAction);
        
        // var result = await repository.GetAsync(new ProductSpec(product.ProductId), cancellationToken);
        var result = await repository.GetAsync(new ProductSellerSpec(requestingUser.Id, product.ProductId), cancellationToken);
        
        if (result == null)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.NotFound, "You are not currently selling this product", ErrorCodes.EntityNotFound));
        
        result.Quantity = product.Quantity ?? result.Quantity;
        result.Discount = product.Discount ?? result.Discount;
        result.Price = product.Price ?? result.Price;
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProduct(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(CommonErrors.GenericUnauthorizedAction);
        
        var countSellers = await repository.GetCountAsync(new ProductSellerSpec(id), cancellationToken);
        if (countSellers != 0)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Conflict, $"Cant delete product, it's sold by {countSellers} sellers"));
        
        await repository.DeleteAsync(new ProductSpec(id), cancellationToken);
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> DeleteProductBySeller(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Seller)
            return ServiceResponse.FromError(CommonErrors.GenericUnauthorizedAction);
        
        await repository.DeleteAsync(new ProductSellerSpec(requestingUser.Id, id), cancellationToken);
        return ServiceResponse.ForSuccess();
    }    
}