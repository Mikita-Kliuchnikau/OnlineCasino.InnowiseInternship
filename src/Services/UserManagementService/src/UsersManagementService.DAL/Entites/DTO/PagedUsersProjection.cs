using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.DAL.Entites.DTO;

public class PagedUsersProjection
{
    public int PageNumber { get; set; }

    public int TotalCount { get; set; }

    public List<UserEntity> Users { get; set; } = [];
}