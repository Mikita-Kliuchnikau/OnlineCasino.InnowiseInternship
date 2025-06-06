using UsersManagementService.Common.Enums;

namespace UsersManagementService.Presentation.Models;

public class UserDto
{
    public Guid Id { get; set; } = Guid.Empty;
    required public Guid AuthId { get; set; }
    required public string Username { get; set; }
    required public string Email { get; set; }
    public decimal? Balance { get; set; }
    public VerificationStatus? VerificationStatus { get; set; }
    public bool? IsBanned { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }  
    public string? LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? PassportNumber { get; set; }
    public string? IdentificationNumber { get; set; }
}
