using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.Feedback;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FeedbackController(IFeedbackService feedbackService, IUserService userService)
    : AuthorizedController(userService)
{
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<FeedbackDTO>>> GetById([FromRoute] Guid id)
    {
        var requestingUser = await GetCurrentUser();

        return requestingUser.Result == null ?
            ErrorMessageResult<FeedbackDTO>(requestingUser.Error) :
            FromServiceResponse(await feedbackService.GetFeedback(id, requestingUser.Result));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] FeedbackAddDTO feedback)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null
            ? FromServiceResponse(await feedbackService.AddFeedback(feedback, currentUser.Result))
            : ErrorMessageResult(currentUser.Error);
    }
    
    [Authorize]
    [HttpGet] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetPage.
    public async Task<ActionResult<RequestResponse<PagedResponse<FeedbackDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination) // The FromQuery attribute will bind the parameters matching the names of
    // the PaginationSearchQueryParams properties to the object in the method parameter.
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            FromServiceResponse(await feedbackService.GetFeedbacks(pagination, currentUser.Result)) :
            ErrorMessageResult<PagedResponse<FeedbackDTO>>(currentUser.Error);
    }
}
