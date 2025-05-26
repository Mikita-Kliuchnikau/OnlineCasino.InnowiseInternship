using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.User;

public record UserViewModel(
    Guid Id,
    string Username,
    string Email,
    decimal Balance,
    VerificationStatus VerificationStatus,
    bool IsBanned,
    bool IsDeleted,
    DateTime CreatedAt,
    string? FirstName = null,
    string? SecondName = null,
    string? LastName = null,
    DateOnly? BirthDate = null,
    string? PassportNumber = null,
    string? IdentificationNumber = null);