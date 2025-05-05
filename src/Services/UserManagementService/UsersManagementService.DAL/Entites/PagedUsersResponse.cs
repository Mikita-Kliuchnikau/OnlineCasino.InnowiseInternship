namespace UsersManagementService.DAL.Entites;

public class PagedUsersResponse
{
    public int PageNumber { get; set; }

    public int TotalCount { get; set; }

    public List<UserEntity> Responce { get; set; } = [];
}
