namespace UsersManagementService.DAL.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}
