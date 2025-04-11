namespace MobyLabWebProgramming.Core.Entities;

public class Feedback : BaseEntity
{
    public Guid MostBoughtProductCategory { get; set; }
    public int GeneralExperience { get; set; }
    public ICollection<string> Functionalities { get; set; } = [];
    public string Suggestions { get; set; } = null!;
}