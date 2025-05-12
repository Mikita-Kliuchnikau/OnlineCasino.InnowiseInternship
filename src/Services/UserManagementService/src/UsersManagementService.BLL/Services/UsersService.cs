using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.DeleteUser;
using UsersManagementService.BLL.Models.User.UpdateUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Interfaces.Repositories;
using UsersManagementService.BLL.Interfaces.Services;
using Mapster;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.DTO;

namespace UsersManagementService.BLL.Services;

public class UsersService(IUsersRepository usersRepository) : IUsersService
{
    public async Task<Guid> CreateUserAsync(CreateUserCommand createUserCommand, CancellationToken cancellationToken = default)
    {
        var user = createUserCommand.Adapt<UserEntity>();

        return await usersRepository.CreateAsync(user, cancellationToken);
    }

    public async Task<Guid> DeleteUserAsync(DeleteUserCommand user, CancellationToken cancellationToken = default)
    {
        return await usersRepository.DeleteAsync(user.Id, cancellationToken);
    }

    public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery getPagedUsersQuery, CancellationToken cancellationToken)
    {
        var pagedUsersfilter = getPagedUsersQuery.Adapt<PagedUsersFilter>();

        var pagedUsersProjection = await usersRepository
           .GetPagedAsync(pagedUsersfilter, cancellationToken);

         return pagedUsersProjection.Adapt<PagedUsersViewModel>();
    }

    public async Task<UserViewModel> GetUserByIdAsync(GetUserQuery getUsersQuery, CancellationToken cancellationToken)
    {
        var userEntity = await usersRepository.GetByIdAsync(getUsersQuery.Id, cancellationToken);

        if (userEntity == null)
        {
            throw new NotFoundException(nameof(userEntity), getUsersQuery.Id);
        }

        return userEntity.Adapt<UserViewModel>();
    }

    public async Task<Guid> UpdateUserAsync(UpdateUserCommand user, CancellationToken cancellationToken)
    {
        return await usersRepository.UpdateAsync(user.Adapt<UserEntity>(), cancellationToken);
    }
}