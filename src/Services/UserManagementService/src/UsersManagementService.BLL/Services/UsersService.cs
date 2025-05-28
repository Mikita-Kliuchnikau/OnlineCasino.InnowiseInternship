using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Interfaces.Repositories;
using UsersManagementService.BLL.Interfaces.Services;
using Mapster;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.Dto;
using UsersManagementService.BLL.Models.User;
using Microsoft.Extensions.Logging;
using static UsersManagementService.Common.Constants.LoggingMessages;

namespace UsersManagementService.BLL.Services;

public class UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger) : IUsersService
{
    public async Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            RequestStartingMessage,
            nameof(CreateUserAsync),
            DateTime.UtcNow);

        var userEntity = user.Adapt<UserEntity>();
        var result =  await usersRepository.CreateAsync(userEntity, cancellationToken);

        logger.LogInformation(
            RequestSucceededMessage,
            nameof(CreateUserAsync),
            result,
            DateTime.UtcNow);

        return result; 
    }

    public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            RequestStartingMessage,
            nameof(DeleteUserAsync),
            DateTime.UtcNow);

        var result = Guid.Empty;

        try
        {
            result = await usersRepository.DeleteAsync(id, cancellationToken);
        }
        catch(NotFoundException ex)
        {
            logger.LogError(
                RequestFailedMessage,
                nameof(DeleteUserAsync),
                ex.Message,
                DateTime.UtcNow);
            throw;
        }

        logger.LogInformation(
            RequestSucceededMessage,
            nameof(DeleteUserAsync),
            id,
            DateTime.UtcNow);

        return result;
    }

    public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            RequestStartingMessage,
            nameof(GetPagedUsersAsync),
            DateTime.UtcNow);

        var pagedUsersfilter = users.Adapt<PagedUsersFilter>();
        var pagedUsersProjection = await usersRepository.GetPagedAsync(pagedUsersfilter, cancellationToken);

        var result = pagedUsersProjection.Adapt<PagedUsersViewModel>();

        logger.LogInformation(
            RequestSucceededMessage,
            nameof(GetPagedUsersAsync),
            result.TotalCount,
            DateTime.UtcNow);

        return result;
    }

    public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            RequestStartingMessage,
            nameof(GetUserByIdAsync),
            DateTime.UtcNow);


        UserEntity userEntity = null!;
        try
        {
            userEntity = await usersRepository.GetByIdAsync(id, cancellationToken);

            if (userEntity == null)
            {
                throw new NotFoundException(nameof(userEntity), id);
            }
        }
        catch (NotFoundException ex)
        {
            logger.LogError(
                RequestFailedMessage,
                nameof(GetUserByIdAsync),
                ex.Message,
                DateTime.UtcNow);
            throw;
        }

        var result = userEntity.Adapt<UserViewModel>();

        logger.LogInformation(
            RequestSucceededMessage,
            nameof(GetUserByIdAsync),
            DateTime.UtcNow);

        return result;
    }

    public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            RequestStartingMessage,
            nameof(UpdateUserAsync),
            DateTime.UtcNow);

        var result = Guid.Empty;

        try
        {
            result = await usersRepository.UpdateAsync(user.Adapt<UserEntity>(), cancellationToken);
        }
        catch(NotFoundException ex)
        {
            logger.LogError(
                RequestFailedMessage,
                nameof(UpdateUserAsync),
                ex.Message,
                DateTime.UtcNow);
            throw;
        }

        logger.LogInformation(
            RequestSucceededMessage,
            nameof(UpdateUserAsync),
            result,
            DateTime.UtcNow);

        return result;
    }
}