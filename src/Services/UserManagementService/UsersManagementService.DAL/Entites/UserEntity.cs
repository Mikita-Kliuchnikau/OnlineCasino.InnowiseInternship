using UsersManagementService.DAL.Interfaces;

namespace UsersManagementService.DAL.Entites;

public class UserEntity : IHaveTimestamps
{
    public Guid Id { get; set; }
    public Guid AuthId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.UnVerified;
    public bool IsBanned { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? PassportNumber { get; set; }
    public string? IdentificationNumber { get; set; }
    public ImageEntity? Image { get; set; }
}

// Will be moved to smth like "Common"
public enum VerificationStatus
{
    Pending = 0,
    Verifies = 1,
    UnVerified = 2,
};
