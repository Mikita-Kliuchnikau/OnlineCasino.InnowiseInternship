using UsersManagementService.BLL.Models.User.Commands.CreateUser;
using UsersManagementService.BLL.Models.User.Commands.DeleteUser;
using UsersManagementService.BLL.Models.User.Commands.UpdateUser;
using UsersManagementService.BLL.Models.User.Queries.GetPagedUsers;
using UsersManagementService.BLL.Models.User.Queries.GetUser;

namespace UsersManagementService.BLL.Interfaces;

public interface IUsersService
{
    Task<UserViewModel> GetUserByIdAsync(GetUserQuery user, CancellationToken cancellationToken);

    Task<PagedUsersViewModel> GetPagedUserAsync(GetPagedUsersQuery users, CancellationToken cancellationToken);

    Task<Guid> CreateUserAsync(CreateUserCommand user, CancellationToken cancellationToken);

    Task<Guid> UpdateUserAsync(UpdateUserCommand user, CancellationToken cancellationToken);

    Task<Guid> DeleteUserAsync(DeleteUserCommand user, CancellationToken cancellationToken);
}