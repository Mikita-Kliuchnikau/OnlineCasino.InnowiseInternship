using MediatR;
using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.User.Commands.UpdateUser;

// Can be divided into separate commands
public record UpdateUserCommand(
    Guid Id, 
    Guid AuthId,
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
    string? IdentificationNumber = null) : IRequest<Guid>{ }