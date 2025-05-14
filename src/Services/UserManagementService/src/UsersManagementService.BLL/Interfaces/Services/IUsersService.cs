using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.UpdateUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;

namespace UsersManagementService.BLL.Interfaces.Services;

public interface IUsersService
{
    Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken);

    Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken);

    Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken);

    Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken);
}