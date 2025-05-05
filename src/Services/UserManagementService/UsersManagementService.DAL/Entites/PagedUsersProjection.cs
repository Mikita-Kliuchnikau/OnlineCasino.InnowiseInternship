namespace UsersManagementService.DAL.Entites;

public class PagedUsersProjection
{
    public int PageNumber { get; set; }

    public int TotalCount { get; set; }

    public List<UserEntity> Projection { get; set; } = [];
}
