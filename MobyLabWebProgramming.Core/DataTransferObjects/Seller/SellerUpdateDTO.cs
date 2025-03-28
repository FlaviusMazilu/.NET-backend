using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects.Seller;

public record SellerUpdateDTO(Guid Id, string? Name, int? CUI, Guid? UserAccountId);