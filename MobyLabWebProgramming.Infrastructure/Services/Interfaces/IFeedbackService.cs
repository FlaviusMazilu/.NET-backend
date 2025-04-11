using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.Feedback;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IFeedbackService
{
    public Task<ServiceResponse<FeedbackDTO>> GetFeedback(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacks(PaginationSearchQueryParams pagination, UserDTO requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddFeedback(FeedbackAddDTO feedback, UserDTO requestingUser, CancellationToken cancellationToken = default);

}
