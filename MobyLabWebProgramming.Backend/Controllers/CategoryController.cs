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
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<List<CategoryDTO>>>> GetAll()
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null
            ? FromServiceResponse(await categoryService.GetCategories())
            : ErrorMessageResult<List<CategoryDTO>>(currentUser.Error);

    }
}