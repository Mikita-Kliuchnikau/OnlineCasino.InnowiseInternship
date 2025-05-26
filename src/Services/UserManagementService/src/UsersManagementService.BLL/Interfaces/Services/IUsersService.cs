using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.BLL.Interfaces.Services;

public interface IUsersService
{
    Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default);

    Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken = default);

    Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default);

    Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
}