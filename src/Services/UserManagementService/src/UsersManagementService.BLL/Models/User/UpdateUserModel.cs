using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.User;

public record UpdateUserModel(
    Guid Id, 
    string AuthId,
    string Username,
    string Email,
    decimal Balance,
    VerificationStatus VerificationStatus,
    string? FirstName = null,
    string? SecondName = null,
    string? LastName = null,
    DateOnly? BirthDate = null,
    string? PassportNumber = null,
    string? IdentificationNumber = null);