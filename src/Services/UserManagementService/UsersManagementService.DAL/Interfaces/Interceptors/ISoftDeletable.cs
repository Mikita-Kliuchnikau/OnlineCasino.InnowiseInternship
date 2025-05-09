namespace UsersManagementService.DAL.Interfaces.Interceptors;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}