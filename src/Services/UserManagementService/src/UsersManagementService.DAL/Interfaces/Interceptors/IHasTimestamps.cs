namespace UsersManagementService.DAL.Interfaces.Interceptors;

public interface IHasTimestamps
{
    DateTime CreatedAt { get; set; }
}