using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.BLL.Interfaces.Services;

public interface IUsersService
{
    Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default);

    Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken = default);

    Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default);

    Task<Guid> BanUserAsync(Guid id, bool isBanned, CancellationToken cancellationToken = default);

    Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> TryChangeUserBalanceAsync(Guid id, decimal amount, CancellationToken cancellationToken = default);

    Task<bool> ExistsUserAsync(Guid id, CancellationToken cancellationToken = default);
}