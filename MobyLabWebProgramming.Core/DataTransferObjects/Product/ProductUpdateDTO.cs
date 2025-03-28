namespace MobyLabWebProgramming.Core.DataTransferObjects.Product;

public record ProductUpdateDTO(Guid Id, Guid? CategoryId, string? Name, string? Description);