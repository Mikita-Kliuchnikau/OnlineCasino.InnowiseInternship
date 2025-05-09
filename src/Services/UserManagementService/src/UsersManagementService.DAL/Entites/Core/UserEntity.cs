using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Interfaces.Interceptors;

namespace UsersManagementService.DAL.Entites.Core;

public class UserEntity : IHasTimestamps, ISoftDeletable
{
    public Guid Id { get; set; }
    public Guid AuthId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.UnVerified;
    public bool IsBanned { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? PassportNumber { get; set; }
    public string? IdentificationNumber { get; set; }
    public List<ImageEntity> Images { get; set; } = [];
}