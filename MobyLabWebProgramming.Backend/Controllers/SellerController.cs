using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.Seller;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SellerController(IUserService userService, ISellerService sellerService) : AuthorizedController(userService)
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<SellerDTO>>> GetById([FromRoute] Guid id)
    {
        return FromServiceResponse(await sellerService.GetSeller(id));
    }

    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<SellerDTO>>>> GetPage([FromQuery] PaginationSearchRelatedEntityQueryParams pagination)
    {
        return FromServiceResponse(await sellerService.GetSellers(pagination));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] SellerAddDTO seller)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            FromServiceResponse(await sellerService.AddSeller(seller, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] SellerUpdateDTO seller)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null
            ? FromServiceResponse(await sellerService.UpdateSeller(seller, currentUser.Result))
            : ErrorMessageResult(currentUser.Error);
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<RequestResponse>> Delete(Guid id)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null
            ? FromServiceResponse(await sellerService.DeleteSeller(id, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }
}