using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.Seller;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class SellerService(IRepository<WebAppDatabaseContext> repository) : ISellerService
{
    public async Task<ServiceResponse<SellerDTO>> GetSeller(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new SellerProjectionSpec(id));

        return result != null
            ? ServiceResponse.ForSuccess(result)
            : ServiceResponse.FromError<SellerDTO>(CommonErrors.SellerNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<SellerDTO>>> GetSellers(PaginationSearchRelatedEntityQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await repository.PageAsync(pagination, new SellerProjectionSpec(pagination.Search, pagination.entityId), cancellationToken);
        return ServiceResponse.ForSuccess<PagedResponse<SellerDTO>>(result);
    }

    public async Task<ServiceResponse<int>> GetSellerCount(CancellationToken cancellationToken = default)
    {
        var result = await repository.GetCountAsync<Seller>(cancellationToken);
        return ServiceResponse.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddSeller(SellerAddDTO seller, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new SellerSpec(seller.CUI));
        if (result != null)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Conflict, "A Seller with the same CUI already registered"));

        var newSeller = new Seller()
        {
            CUI = seller.CUI,
            Name = seller.Name,
            UserAccountId = requestingUser.Id
        };

        await repository.AddAsync(newSeller, cancellationToken);
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateSeller(SellerUpdateDTO seller, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != seller.UserAccountId) // Verify who can add the user, you can change this however you se fit.
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the Seller!", ErrorCodes.CannotUpdate));
        
        var result = await repository.GetAsync(new SellerSpec(seller.Id), cancellationToken);
        
        if (result == null)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.NotFound, "Seller does not exist"));

        if (seller.UserAccountId != null)
        {
            var oldSeller = await repository.GetAsync(new SellerSpec(seller.Id), cancellationToken);
            if (oldSeller != null)
            {
                oldSeller.UserAccount.Role = UserRoleEnum.Client;
                await repository.UpdateAsync(oldSeller, cancellationToken);                
            }
        }
        result.Name = seller.Name ?? result.Name;
        result.CUI = seller.CUI ?? result.CUI;
        result.UserAccountId = seller.UserAccountId ?? result.UserAccountId;
        
        await repository.UpdateAsync(result, cancellationToken);
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteSeller(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id) // Verify who can add the user, you can change this however you se fit.
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the Seller!", ErrorCodes.CannotUpdate));
        
        var result = await repository.GetAsync(new SellerSpec(id, includeUser:true), cancellationToken);
        
        if (result == null)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.NotFound, "Seller does not exist"));
        
        result.UserAccount.Role = UserRoleEnum.Client;
        await repository.UpdateAsync(result, cancellationToken);
        await repository.DeleteAsync<Seller>(id, cancellationToken);
        
        return ServiceResponse.ForSuccess();
;    }
}