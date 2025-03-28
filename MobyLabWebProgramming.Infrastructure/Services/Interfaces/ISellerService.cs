using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.Seller;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ISellerService
{
 /// <summary>
    /// GetSeller will provide the information about a seller given its Id.
    /// User should be Admin or Seller.Id = requestinGUser.Id
    /// </summary>
    public Task<ServiceResponse<SellerDTO>> GetSeller(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetSellers returns page with sellers information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<SellerDTO>>> GetSellers(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetSellerCount returns the number of users in the database.
    /// </summary>
    public Task<ServiceResponse<int>> GetSellerCount(CancellationToken cancellationToken = default);
    /// <summary>
    /// AddSellers adds a seller and verifies if requesting user has permissions to add one.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddSeller(SellerAddDTO seller, UserDTO requestingUser, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateSeller updates a seller and verifies if requesting user has permissions to update it, if the user is his own then that should be allowed.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateSeller(SellerUpdateDTO seller, UserDTO requestingUser, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteSeller deletes a seller and verifies if requesting user has permissions to delete it, if the user is his own then that should be allowed.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteSeller(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);   
}