using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
    public static ErrorMessage CategoryNotFound => new(HttpStatusCode.NotFound, "Category doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage UnauthorizedAdminAction => new(HttpStatusCode.Forbidden, "Only an admin can perform this action", ErrorCodes.AdminActionOnly);
    public static ErrorMessage GenericUnauthorizedAction => new(HttpStatusCode.Forbidden, "You have not enough permissions to perfom this action", ErrorCodes.AdminActionOnly);
    public static ErrorMessage ProductNotFound => new(HttpStatusCode.NotFound, "Product doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage EntityNameAlreadyExists => new(HttpStatusCode.Conflict, "An entity of this type already exists with this name");
    public static ErrorMessage SellerNotFound => new(HttpStatusCode.NotFound, "Seller doesn't exist!", ErrorCodes.EntityNotFound);
}
