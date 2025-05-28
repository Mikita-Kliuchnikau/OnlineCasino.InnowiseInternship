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
        var userEntity = user.Adapt<UserEntity>();
        
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}, {@DateTime}",
            nameof(CreateUserAsync),
            userEntity,
            DateTime.UtcNow);

        var result =  await usersRepository.CreateAsync(userEntity, cancellationToken);

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}, {@DateTime}",
            nameof(CreateUserAsync),
            result,
            DateTime.UtcNow);

        return result; 
    }

    public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}, {@DateTime}",
            nameof(DeleteUserAsync),
            id,
            DateTime.UtcNow);

        var result = await usersRepository.DeleteAsync(id, cancellationToken);

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}, {@DateTime}",
            nameof(DeleteUserAsync),
            result,
            DateTime.UtcNow);

        return result;
    }

    public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
    {
        var pagedUsersfilter = users.Adapt<PagedUsersFilter>();
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}, {@DateTime}",
            nameof(GetPagedUsersAsync),
            pagedUsersfilter,
            DateTime.UtcNow);

        var pagedUsersProjection = await usersRepository.GetPagedAsync(pagedUsersfilter, cancellationToken);
        var result = pagedUsersProjection.Adapt<PagedUsersViewModel>();

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}, {@DateTime}",
            nameof(GetPagedUsersAsync),
            result,
            DateTime.UtcNow);

        return result;
    }

    public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}, {@DateTime}",
            nameof(GetUserByIdAsync),
            id,
            DateTime.UtcNow);

        var userEntity = await usersRepository.GetByIdAsync(id, cancellationToken);

        var result = userEntity.Adapt<UserViewModel>();

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}, {@DateTime}",
            nameof(GetUserByIdAsync),
            result,
            DateTime.UtcNow);

        return result;
    }

    public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}, {@DateTime}",
            nameof(UpdateUserAsync),
            user,
            DateTime.UtcNow);

        var result = await usersRepository.UpdateAsync(user.Adapt<UserEntity>(), cancellationToken);

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}, {@DateTime}",
            nameof(UpdateUserAsync),
            result,
            DateTime.UtcNow);

        return result;
    }
}