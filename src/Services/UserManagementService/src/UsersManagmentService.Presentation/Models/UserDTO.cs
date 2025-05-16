using UsersManagementService.Common.Enums;

namespace UsersManagmentService.Presentation.Models;

public record UserDTO(
    Guid Id,
    Guid AuthId,
    string Username,
    string Email,
    decimal Balance,
    VerificationStatus? VerificationStatus,
    bool? IsBanned,
    string? FirstName = null,
    string? SecondName = null,
    string? LastName = null,
    DateOnly? BirthDate = null,
    string? PassportNumber = null,
    string? IdentificationNumber = null);
