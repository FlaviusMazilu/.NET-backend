namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CategoryDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}