using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.BLL.Models.User.UpdateUser;

// Can be divided into separate commands
public record UpdateUserCommand(
    Guid Id, 
    Guid AuthId,
    string Username,
    string Email,
    decimal Balance,
    VerificationStatus VerificationStatus,
    bool IsBanned,
    bool IsDeleted,
    string? FirstName = null,
    string? SecondName = null,
    string? LastName = null,
    DateOnly? BirthDate = null,
    string? PassportNumber = null,
    string? IdentificationNumber = null,
    List<ImageEntity>? Images = null);