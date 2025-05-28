using FluentValidation;
using Microsoft.Extensions.Logging;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User;

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
                "Processing validation {ValidationName}, {@Model}", 
                nameof(CreateUserAsync),
                user);

            await createUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);


            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}",
                nameof(CreateUserAsync),
                user);

            return await usersService.CreateUserAsync(user, cancellationToken);
        }

        public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var deleteUserModelValidator = usersValidator.GetUserIdValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {ValidationName}, {@Model}", 
                nameof(DeleteUserAsync),
                id);

            await deleteUserModelValidator.ValidateAndThrowAsync(id, cancellationToken);

            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}",
                nameof(DeleteUserAsync),
                id);

            return await usersService.DeleteUserAsync(id, cancellationToken);
        }

        public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
        {
            var getPagedUsersQueryValidator = usersValidator.GetPagedUsersQueryValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {ValidationName}, {@Model}", 
                nameof(GetPagedUsersAsync),
                users);

            await getPagedUsersQueryValidator.ValidateAndThrowAsync(users, cancellationToken);

            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}",
                nameof(GetPagedUsersAsync),
                users);

            return await usersService.GetPagedUsersAsync(users, cancellationToken);
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var getUserQueryValidator = usersValidator.GetUserIdValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {ValidationName}, {@Model}",
                nameof(GetUserByIdAsync),
                id);

            await getUserQueryValidator.ValidateAndThrowAsync(id, cancellationToken);

            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}",
                nameof(GetUserByIdAsync),
                id);

            return await usersService.GetUserByIdAsync(id, cancellationToken);
        }

        public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
        {
            var updateUserModelValidator = usersValidator.GetUpdateUserModelValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {ValidationName}, {@Model}", 
                nameof(UpdateUserAsync),
                user);

            await updateUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);

            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}",
                nameof(UpdateUserAsync),
                user);

            return await usersService.UpdateUserAsync(user, cancellationToken);
        }
    }
}
