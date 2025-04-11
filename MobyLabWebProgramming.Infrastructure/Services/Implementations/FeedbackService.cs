using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.Feedback;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class FeedbackService(IRepository<WebAppDatabaseContext> repository) : IFeedbackService
{
    public async Task<ServiceResponse<FeedbackDTO>> GetFeedback(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError<FeedbackDTO>(CommonErrors.GenericUnauthorizedAction);
        
        var feedback = await repository.GetAsync(new FeedbackProjectionSpec(id), cancellationToken);
        return feedback != null
            ? ServiceResponse.ForSuccess(feedback)
            : ServiceResponse.FromError<FeedbackDTO>(CommonErrors.FeedbackNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacks(PaginationSearchQueryParams pagination, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError<PagedResponse<FeedbackDTO>>(CommonErrors.GenericUnauthorizedAction);

        var feedback = await repository.PageAsync(pagination, new FeedbackProjectionSpec(), cancellationToken);
        return ServiceResponse.ForSuccess(feedback);

    }

    public async Task<ServiceResponse> AddFeedback(FeedbackAddDTO feedback, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var newFeedback = new Feedback()
        {
            Functionalities = feedback.Functionalities,
            Suggestions = feedback.Suggestions,
            GeneralExperience = feedback.GeneralExperience,
            MostBoughtProductCategory = feedback.MostBoughtProductCategory
        };
        
        await repository.AddAsync(newFeedback, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
}