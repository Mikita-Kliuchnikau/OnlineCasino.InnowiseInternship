using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.User.Queries.GetUser;

public record UserViewModel(
    Guid Id,
    string Username,
    string Email,
    decimal Balance,
    VerificationStatusEnum VerificationStatus,
    bool IsBanned,
    bool IsDeleted,
    string? FirstName = null,
    string? SecondName = null,
    string? LastName = null,
    DateOnly? BirthDate = null,
    string? PassportNumber = null,
    string? IdentificationNumber = null,
    List<ImageViewModel>? Images = null);