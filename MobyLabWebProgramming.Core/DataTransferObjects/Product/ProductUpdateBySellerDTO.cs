namespace MobyLabWebProgramming.Core.DataTransferObjects.Product;

public record ProductUpdateBySellerDTO(Guid ProductId, float? Price, int? Quantity, float? Discount);