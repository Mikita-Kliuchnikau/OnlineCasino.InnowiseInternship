using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Interfaces.Repositories;
using UsersManagementService.BLL.Interfaces.Services;
using Mapster;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.Dto;
using UsersManagementService.BLL.Models.User;
using Microsoft.Extensions.Logging;

namespace UsersManagementService.BLL.Services;

public class UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger) : IUsersService
{
    public async Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {@RequestName}, {@DateTime}",
            nameof(CreateUserAsync),
            DateTime.UtcNow);

        var userEntity = user.Adapt<UserEntity>();
        var result =  await usersRepository.CreateAsync(userEntity, cancellationToken);

        logger.LogInformation(
            "Complited request {@RequestName}, {@DateTime}",
            nameof(CreateUserAsync),
            DateTime.UtcNow);

        return result; 
    }

    public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {@RequestName}, {@DateTime}",
            nameof(DeleteUserAsync),
            DateTime.UtcNow);

        var result = await usersRepository.DeleteAsync(id, cancellationToken);

        logger.LogInformation(
            "Complited request {@RequestName}, {@DateTime}",
            nameof(DeleteUserAsync),
            DateTime.UtcNow);

        return result;
    }

    public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {@RequestName}, {@DateTime}",
            nameof(GetPagedUsersAsync),
            DateTime.UtcNow);

        var pagedUsersfilter = users.Adapt<PagedUsersFilter>();
        var pagedUsersProjection = await usersRepository.GetPagedAsync(pagedUsersfilter, cancellationToken);
        var result = pagedUsersProjection.Adapt<PagedUsersViewModel>();

        logger.LogInformation(
            "Complited request {@RequestName}, {@DateTime}",
            nameof(GetPagedUsersAsync),
            DateTime.UtcNow);

        return result;
    }

    public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {@RequestName}, {@DateTime}",
            nameof(GetUserByIdAsync),
            DateTime.UtcNow);


        var userEntity = await usersRepository.GetByIdAsync(id, cancellationToken);

        var result = userEntity.Adapt<UserViewModel>();

        logger.LogInformation(
            "Complited request {@RequestName}, {@DateTime}",
            nameof(GetUserByIdAsync),
            DateTime.UtcNow);

        return result;
    }

    public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {@RequestName}, {@DateTime}",
            nameof(UpdateUserAsync),
            DateTime.UtcNow);

        var result = await usersRepository.UpdateAsync(user.Adapt<UserEntity>(), cancellationToken);

        logger.LogInformation(
            "Complited request {@RequestName}, {@DateTime}",
            nameof(UpdateUserAsync),
            DateTime.UtcNow);

        return result;
    }
}