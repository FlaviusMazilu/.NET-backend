using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects.Feedback;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class FeedbackProjectionSpec : Specification<Feedback, FeedbackDTO>
{
    public FeedbackProjectionSpec(bool orderByCreatedAt = false) => Query.Select(e => new FeedbackDTO()
        {
            Id = e.Id,
            Functionalities = e.Functionalities,
            Suggestions = e.Suggestions,
            GeneralExperience = e.GeneralExperience,
            MostBoughtProductCategory = e.MostBoughtProductCategory,
        })
        .OrderByDescending(e => e.CreatedAt, orderByCreatedAt);
    
    public FeedbackProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);

}