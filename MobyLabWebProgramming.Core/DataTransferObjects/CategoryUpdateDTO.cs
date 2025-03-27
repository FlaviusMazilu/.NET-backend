namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record CategoryUpdateDTO(Guid id, string? Name = null, string? Description = null);