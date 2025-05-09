using UsersManagementService.BLL.Extensions.MappingExtensions;
using UsersManagementService.BLL.Interfaces;
using UsersManagementService.BLL.Models.User.Commands.CreateUser;
using UsersManagementService.BLL.Models.User.Commands.DeleteUser;
using UsersManagementService.BLL.Models.User.Commands.UpdateUser;
using UsersManagementService.BLL.Models.User.Queries.GetPagedUsers;
using UsersManagementService.BLL.Models.User.Queries.GetUser;
using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Services;

public class UsersService(IUsersRepository usersRepository) : IUsersService
{
    public async Task<Guid> CreateUserAsync(CreateUserCommand createUserCommand, CancellationToken cancellationToken = default)
    {
        var user = createUserCommand.ToUserEntity();

        return await usersRepository.CreateAsync(user, cancellationToken);
    }

    public async Task<Guid> DeleteUserAsync(DeleteUserCommand user, CancellationToken cancellationToken = default)
    {
        return await usersRepository.DeleteAsync(user.Id, cancellationToken);
    }

    public async Task<PagedUsersViewModel> GetPagedUserAsync(GetPagedUsersQuery users, CancellationToken cancellationToken)
    {
        var pagedUsersfilter = users.ToPagedUsersFilter();

        var pagedUsersProjection = await usersRepository
           .GetPagedAsync(pagedUsersfilter, cancellationToken);

         return pagedUsersProjection.ToPagedUsersViewModel();
    }

    public async Task<UserViewModel> GetUserByIdAsync(GetUserQuery user, CancellationToken cancellationToken)
    {
        var userEntity = await usersRepository.GetByIdAsync(user.Id, cancellationToken);

        if (userEntity == null)
        {
            throw new NotFoundException(nameof(userEntity), user.Id);
        }

        return userEntity.ToUserViewModel();
    }

    public async Task<Guid> UpdateUserAsync(UpdateUserCommand user, CancellationToken cancellationToken)
    {
        return await usersRepository.UpdateAsync(user.ToUserEntity(), cancellationToken);
    }
}