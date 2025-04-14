using MobyLabWebProgramming.Core.DataTransferObjects.Seller;

namespace MobyLabWebProgramming.Core.DataTransferObjects.Product;

public class ProductWSellersDTO : ProductDTO
{
    public ICollection<SellerDTO> Sellers { get; set; }
}