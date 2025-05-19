using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.UpdateUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Interfaces.Repositories;
using UsersManagementService.BLL.Interfaces.Services;
using Mapster;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.Dto;

namespace UsersManagementService.BLL.Services;

public class UsersService(IUsersRepository usersRepository) : IUsersService
{
    public async Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken = default)
    {
        var userEntity = user.Adapt<UserEntity>();

        return await usersRepository.CreateAsync(userEntity, cancellationToken);
    }

    public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await usersRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
    {
        var pagedUsersfilter = users.Adapt<PagedUsersFilter>();

        var pagedUsersProjection = await usersRepository
           .GetPagedAsync(pagedUsersfilter, cancellationToken);

         return pagedUsersProjection.Adapt<PagedUsersViewModel>();
    }

    public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var userEntity = await usersRepository.GetByIdAsync(id, cancellationToken);

        if (userEntity == null)
        {
            throw new NotFoundException(nameof(userEntity), id);
        }

        return userEntity.Adapt<UserViewModel>();
    }

    public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
    {
        return await usersRepository.UpdateAsync(user.Adapt<UserEntity>(), cancellationToken);
    }
}