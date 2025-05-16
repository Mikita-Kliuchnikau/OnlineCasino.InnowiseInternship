using UsersManagementService.Common.Enums;

namespace UsersManagmentService.Presentation.Models;

public class UserDto
{
    required public Guid Id;
    required public Guid AuthId;
    required public string Username;
    required public string Email;
    public decimal? Balance;
    public VerificationStatus? VerificationStatus;
    public bool? IsBanned;
    public string? FirstName = null;
    public string? SecondName = null;
    public string? LastName = null;
    public DateOnly? BirthDate = null;
    public string? PassportNumber = null;
    public string? IdentificationNumber = null;
}
