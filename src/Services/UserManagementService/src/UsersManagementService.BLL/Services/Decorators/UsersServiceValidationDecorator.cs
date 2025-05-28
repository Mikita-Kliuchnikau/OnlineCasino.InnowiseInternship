using FluentValidation;
using Microsoft.Extensions.Logging;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User;
using static UsersManagementService.Common.Constants.LoggingMessages;

namespace UsersManagementService.BLL.Services.Decorators
{
    public class UsersServiceValidationDecorator(
        IUsersService usersService, 
        IUsersValidator usersValidator,
        ILogger<UsersServiceValidationDecorator> logger) : IUsersService
    {
        public async Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken = default)
        {
            var createUserModelValidator = usersValidator.GetCreateUserModelValidatorOrThrow();

            logger.LogDebug(
                ValidatingStartingMessage, 
                nameof(CreateUserAsync),
                nameof(CreateUserModel),
                DateTime.UtcNow);

            try
            {
                await createUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);
            }
            catch (ValidationException ex)
            {
                logger.LogError(string.Format(
                    ValidatingFailedMessage,
                    nameof(CreateUserAsync),
                    nameof(CreateUserModel),
                    ex.Errors,
                    DateTime.UtcNow));
                throw;
            }

            logger.LogDebug(
                ValidatingSucceededMessage,
                nameof(CreateUserAsync),
                nameof(CreateUserModel),
                DateTime.UtcNow);

            return await usersService.CreateUserAsync(user, cancellationToken);
        }

        public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var deleteUserModelValidator = usersValidator.GetUserIdValidatorOrThrow();

            logger.LogDebug(
                ValidatingStartingMessage, 
                nameof(DeleteUserAsync),
                nameof(Guid),
                DateTime.UtcNow);

            try
            {
                await deleteUserModelValidator.ValidateAndThrowAsync(id, cancellationToken);
            }
            catch (ValidationException ex)
            {
                logger.LogError(
                    ValidatingFailedMessage,
                    nameof(DeleteUserAsync),
                    nameof(Guid),
                    ex.Errors,
                    DateTime.UtcNow);
                throw;
            }

            logger.LogDebug(
                ValidatingSucceededMessage,
                nameof(DeleteUserAsync),
                nameof(Guid),
                DateTime.UtcNow);

            return await usersService.DeleteUserAsync(id, cancellationToken);
        }

        public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
        {
            var getPagedUsersQueryValidator = usersValidator.GetPagedUsersQueryValidatorOrThrow();

            logger.LogDebug(
                ValidatingStartingMessage, 
                nameof(GetPagedUsersAsync),
                nameof(GetPagedUsersQuery),
                DateTime.UtcNow);

            try
            {
                await getPagedUsersQueryValidator.ValidateAndThrowAsync(users, cancellationToken);
            }
            catch (ValidationException ex)
            {
                logger.LogError(
                    ValidatingFailedMessage,
                    nameof(GetPagedUsersAsync),
                    nameof(GetPagedUsersQuery),
                    ex.Errors,
                    DateTime.UtcNow);
                throw;
            }

            logger.LogDebug(
                ValidatingSucceededMessage,
                nameof(GetPagedUsersAsync),
                nameof(GetPagedUsersQuery),
                DateTime.UtcNow);

            return await usersService.GetPagedUsersAsync(users, cancellationToken);
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var getUserQueryValidator = usersValidator.GetUserIdValidatorOrThrow();

            logger.LogDebug(
                ValidatingStartingMessage,
                nameof(GetUserByIdAsync),
                nameof(Guid),
                DateTime.UtcNow);

            try
            {
                await getUserQueryValidator.ValidateAndThrowAsync(id, cancellationToken);
            }
            catch (ValidationException ex)
            {
                logger.LogError(
                    ValidatingFailedMessage,
                    nameof(GetUserByIdAsync),
                    nameof(Guid),
                    ex.Errors,
                    DateTime.UtcNow);
                throw;
            }

            logger.LogDebug(
                ValidatingSucceededMessage,
                nameof(GetUserByIdAsync),
                nameof(Guid),
                DateTime.UtcNow);

            return await usersService.GetUserByIdAsync(id, cancellationToken);
        }

        public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
        {
            var updateUserModelValidator = usersValidator.GetUpdateUserModelValidatorOrThrow();

            logger.LogDebug(
                ValidatingStartingMessage, 
                nameof(UpdateUserAsync),
                nameof(UpdateUserModel),
                DateTime.UtcNow);

            try
            {
                await updateUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);
            }
            catch (ValidationException ex)
            {
                logger.LogError(
                    ValidatingFailedMessage,
                    nameof(CreateUserAsync),
                    nameof(UpdateUserModel),
                    ex.Errors,
                    DateTime.UtcNow);
                throw;
            }

            logger.LogDebug(
                ValidatingSucceededMessage,
                nameof(UpdateUserAsync),
                nameof(UpdateUserModel),
                DateTime.UtcNow);

            return await usersService.UpdateUserAsync(user, cancellationToken);
        }
    }
}
