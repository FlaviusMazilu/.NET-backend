using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects.Product;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class ProductController(IUserService userService, IProductService productService) : AuthorizedController(userService)
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ProductDTO>>> GetById(Guid id)
    {
        return FromServiceResponse(await productService.GetProduct(id));
    }
    
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ProductDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        return FromServiceResponse(await productService.GetProducts(pagination));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<RequestResponse>> AddProductBySeller(ProductAddDTO product)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ? 
            FromServiceResponse(await productService.AddProduct(product, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<RequestResponse>> CreateProduct(ProductCreateDTO product)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            FromServiceResponse(await productService.CreateProduct(product, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }

    [HttpGet]
    public async Task<ActionResult<RequestResponse<int>>> GetCount()
    {
        return FromServiceResponse(await productService.GetProductsCount());
    }

    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ProductUpdateDTO product)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            FromServiceResponse(await productService.UpdateProduct(product, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }

    [HttpPost]
    public async Task<ActionResult<RequestResponse>> UpdateBySeller([FromBody] ProductUpdateBySellerDTO product)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            FromServiceResponse(await productService.UpdateProductBySeller(product, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }

    [HttpDelete]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            FromServiceResponse(await productService.DeleteProduct(id, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }
    
    [HttpDelete]
    public async Task<ActionResult<RequestResponse>> DeleteBySeller([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            FromServiceResponse(await productService.DeleteProductBySeller(id, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }
    
    
}