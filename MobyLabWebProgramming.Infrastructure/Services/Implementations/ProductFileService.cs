using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Http;
using MobyLabWebProgramming.Core.DataTransferObjects.Product;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

/// <summary>
/// Inject the required services through the constructor.
/// </summary>
public class ProductFileService(IRepository<WebAppDatabaseContext> repository, IFileRepository fileRepository) : IProductFileService
{
    /// <summary>
    /// This static method creates the path for a user to where it has to store the files, each user should have an own folder.
    /// </summary>
    private static string GetFileDirectory(Guid productId) => Path.Join(productId.ToString(), IProductFileService.ProductFilesDirectory);

    public async Task<ServiceResponse> SaveImage(ProductImageAddDTO file, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only administrators can upload images"));

        var product = await repository.GetAsync<Product>(new ProductSpec(file.productId), cancellationToken);
        if (product == null)
            return ServiceResponse.FromError(CommonErrors.ProductNotFound);
        
        var fileName = fileRepository.SaveFile(file.File, GetFileDirectory(file.productId)); // First save the file on the filesystem.

        if (fileName.Result == null) // If not successful respond with the error.
        {
            return fileName.ToResponse();
        }
        product.PicturePath = fileName.Result;
        await repository.UpdateAsync(product, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<FileDTO>> GetImageDownload(Guid id, CancellationToken cancellationToken = default) // If not successful respond with the error.
    {
        var product = await repository.GetAsync<Product>(new ProductSpec(id), cancellationToken);
        
        if (product?.PicturePath == null)
            return ServiceResponse.FromError<FileDTO>(CommonErrors.ProductNotFound);

        return fileRepository.GetFile(Path.Join(GetFileDirectory(id), product.PicturePath));
    }
}
