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
                "Processing validation {ValidationName}, {@Model}, {@DateTime}", 
                nameof(CreateUserAsync),
                user,
                DateTime.UtcNow);

            await createUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);


            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}, {@DateTime}",
                nameof(CreateUserAsync),
                user,
                DateTime.UtcNow);

            return await usersService.CreateUserAsync(user, cancellationToken);
        }

        public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var deleteUserModelValidator = usersValidator.GetUserIdValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {ValidationName}, {@Model}, {@DateTime}", 
                nameof(DeleteUserAsync),
                id,
                DateTime.UtcNow);

            await deleteUserModelValidator.ValidateAndThrowAsync(id, cancellationToken);

            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}, {@DateTime}",
                nameof(DeleteUserAsync),
                id,
                DateTime.UtcNow);

            return await usersService.DeleteUserAsync(id, cancellationToken);
        }

        public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
        {
            var getPagedUsersQueryValidator = usersValidator.GetPagedUsersQueryValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {ValidationName}, {@Model}, {@DateTime}", 
                nameof(GetPagedUsersAsync),
                users,
                DateTime.UtcNow);

            await getPagedUsersQueryValidator.ValidateAndThrowAsync(users, cancellationToken);

            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}, {@DateTime}",
                nameof(GetPagedUsersAsync),
                users,
                DateTime.UtcNow);

            return await usersService.GetPagedUsersAsync(users, cancellationToken);
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var getUserQueryValidator = usersValidator.GetUserIdValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {ValidationName}, {@Model}, {@DateTime}",
                nameof(GetUserByIdAsync),
                id,
                DateTime.UtcNow);

            await getUserQueryValidator.ValidateAndThrowAsync(id, cancellationToken);

            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}, {@DateTime}",
                nameof(GetUserByIdAsync),
                id,
                DateTime.UtcNow);

            return await usersService.GetUserByIdAsync(id, cancellationToken);
        }

        public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
        {
            var updateUserModelValidator = usersValidator.GetUpdateUserModelValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {ValidationName}, {@Model}, {@DateTime}", 
                nameof(UpdateUserAsync),
                user,
                DateTime.UtcNow);

            await updateUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);

            logger.LogDebug(
                "Complited validation {ValidationName}, {@Model}, {@DateTime}",
                nameof(UpdateUserAsync),
                user,
                DateTime.UtcNow);

            return await usersService.UpdateUserAsync(user, cancellationToken);
        }
    }
}
