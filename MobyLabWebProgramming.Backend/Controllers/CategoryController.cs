using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController(ICategoryService categoryService, IUserService userService)
    : AuthorizedController(userService)
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<CategoryDTO>>> GetById([FromRoute] Guid id)
    {
        return FromServiceResponse(await categoryService.GetCategory(id));
    }
    
    [HttpGet]
    public async Task<ActionResult<RequestResponse<List<CategoryDTO>>>> GetAll()
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null
            ? FromServiceResponse(await categoryService.GetCategories())
            : ErrorMessageResult<List<CategoryDTO>>(currentUser.Error);

    }
    
    [HttpGet(template: "{id:Guid}")]
    public async Task<ActionResult<RequestResponse<CategoryDTO>>> GetCategory([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null
            ? FromServiceResponse(await categoryService.GetCategory(id))
            : ErrorMessageResult<CategoryDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] CategoryAddDTO category)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null
            ? FromServiceResponse(await categoryService.AddCategory(category, currentUser.Result))
            : ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] CategoryUpdateDTO category)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null
            ? FromServiceResponse(await categoryService.UpdateCategory(category, currentUser.Result))
            : ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<RequestResponse>> Delete(Guid id)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null
            ? FromServiceResponse(await categoryService.DeleteCategory(id, currentUser.Result)) :
            ErrorMessageResult(currentUser.Error);
    }
}