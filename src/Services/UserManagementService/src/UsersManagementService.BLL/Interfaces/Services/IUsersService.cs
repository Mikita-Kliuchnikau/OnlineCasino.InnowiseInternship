using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.DeleteUser;
using UsersManagementService.BLL.Models.User.UpdateUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;

namespace UsersManagementService.BLL.Interfaces.Services;

public interface IUsersService
{
    Task<UserViewModel> GetUserByIdAsync(GetUserQuery user, CancellationToken cancellationToken);

    Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken);

    Task<Guid> CreateUserAsync(CreateUserCommand user, CancellationToken cancellationToken);

    Task<Guid> UpdateUserAsync(UpdateUserCommand user, CancellationToken cancellationToken);

    Task<Guid> DeleteUserAsync(DeleteUserCommand user, CancellationToken cancellationToken);
}