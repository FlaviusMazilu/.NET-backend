namespace MobyLabWebProgramming.Core.DataTransferObjects.Feedback;

public class FeedbackDTO
{
    public Guid Id { get; set; }
    public Guid MostBoughtProductCategory { get; set; }
    public int GeneralExperience { get; set; }
    public ICollection<string> Functionalities { get; set; } = [];
    public string Suggestions { get; set; } = null!;
}