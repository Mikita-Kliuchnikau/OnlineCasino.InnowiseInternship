using UsersManagementService.DAL.Interfaces.Repositories;
using UsersManagementService.BLL.Interfaces.Services;
using Mapster;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.Dto;
using UsersManagementService.BLL.Models.User;
using Microsoft.Extensions.Logging;
using UsersManagementService.BLL.Attributes;
using UsersManagementService.BLL.Validators.UsersValidators;
using Grpc.Core;
using Azure.Core;
using UsersManagementService.Common.Exceptions;

namespace UsersManagementService.BLL.Services;

public class UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger) : IUsersService
{
    [Validate(typeof(CreateUserModelValidator))]
    public virtual async Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken = default)
    {
        var userEntity = user.Adapt<UserEntity>();
        
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}",
            nameof(CreateUserAsync),
            userEntity);

        var result =  await usersRepository.CreateAsync(userEntity, cancellationToken);

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}",
            nameof(CreateUserAsync),
            result);

        return result; 
    }

    [Validate(typeof(UserIdValidator))]
    public virtual async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}",
            nameof(DeleteUserAsync),
            id);

        var result = await usersRepository.DeleteAsync(id, cancellationToken);

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}",
            nameof(DeleteUserAsync),
            result);

        return result;
    }

    [Validate(typeof(GetPagedUsersQueryValidator))]
    public virtual async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
    {
        var pagedUsersfilter = users.Adapt<PagedUsersFilter>();
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}",
            nameof(GetPagedUsersAsync),
            pagedUsersfilter);

        var pagedUsersProjection = await usersRepository.GetPagedAsync(pagedUsersfilter, cancellationToken);
        var result = pagedUsersProjection.Adapt<PagedUsersViewModel>();

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}",
            nameof(GetPagedUsersAsync),
            result);

        return result;
    }

    [Validate(typeof(UserIdValidator))]
    public virtual async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}",
            nameof(GetUserByIdAsync),
            id);

        var userEntity = await usersRepository.GetByIdAsync(id, cancellationToken);

        var result = userEntity.Adapt<UserViewModel>();

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}",
            nameof(GetUserByIdAsync),
            result);

        return result;
    }

    [Validate(typeof(UpdateUserModelValidator))]
    public virtual async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}",
            nameof(UpdateUserAsync),
            user);

        var result = await usersRepository.UpdateAsync(user.Adapt<UserEntity>(), cancellationToken);

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}",
            nameof(UpdateUserAsync),
            result);

        return result;
    }

    [Validate(typeof(UserIdValidator))]
    public virtual async Task<Guid> BanUserAsync(Guid id, bool isBanned, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}",
            nameof(UpdateUserAsync),
            id);

        var result = await usersRepository.BanAsync(id, isBanned, cancellationToken);

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}",
            nameof(UpdateUserAsync),
            result);

        return result;
   
    }

    [Validate(typeof(UserIdValidator))]
    public virtual async Task<bool> TryChangeUserBalanceAsync(Guid id, decimal amount, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}",
            nameof(UpdateUserAsync),
        id);

        var userExists = await usersRepository.ExistsAsync(id, cancellationToken);

        if (!userExists)
        {
            throw new NotFoundException();
        }

        var result = await usersRepository.TryChangeBalance(id, amount, cancellationToken);

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}",
            nameof(UpdateUserAsync),
            result);

        return result;
    }

    [Validate(typeof(UserIdValidator))]
    public virtual async Task<bool> ExistsUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing request {RequestName}, {@Model}",
            nameof(ExistsUserAsync),
            id);

        var result = await usersRepository.ExistsAsync(id, cancellationToken);

        logger.LogInformation(
            "Complited request {RequestName} with result {@Result}",
            nameof(ExistsUserAsync),
            result);

        return result;
    }
}