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
                "Processing validation {@ValidationName}, {@ModelName}, {@DateTime}", 
                nameof(CreateUserAsync),
                nameof(CreateUserModel),
                DateTime.UtcNow);

            await createUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);


            logger.LogDebug(
                "Complited validation {@ValidationName}, {@ModelName}, {@DateTime}",
                nameof(CreateUserAsync),
                nameof(CreateUserModel),
                DateTime.UtcNow);

            return await usersService.CreateUserAsync(user, cancellationToken);
        }

        public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var deleteUserModelValidator = usersValidator.GetUserIdValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {@ValidationName}, {@ModelName}, {@DateTime}", 
                nameof(DeleteUserAsync),
                nameof(Guid),
                DateTime.UtcNow);

            await deleteUserModelValidator.ValidateAndThrowAsync(id, cancellationToken);

            logger.LogDebug(
                "Complited validation {@ValidationName}, {@ModelName}, {@DateTime}",
                nameof(DeleteUserAsync),
                nameof(Guid),
                DateTime.UtcNow);

            return await usersService.DeleteUserAsync(id, cancellationToken);
        }

        public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
        {
            var getPagedUsersQueryValidator = usersValidator.GetPagedUsersQueryValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {@ValidationName}, {@ModelName}, {@DateTime}", 
                nameof(GetPagedUsersAsync),
                nameof(GetPagedUsersQuery),
                DateTime.UtcNow);

            await getPagedUsersQueryValidator.ValidateAndThrowAsync(users, cancellationToken);

            logger.LogDebug(
                "Complited validation {@ValidationName}, {@ModelName}, {@DateTime}",
                nameof(GetPagedUsersAsync),
                nameof(GetPagedUsersQuery),
                DateTime.UtcNow);

            return await usersService.GetPagedUsersAsync(users, cancellationToken);
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var getUserQueryValidator = usersValidator.GetUserIdValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {@ValidationName}, {@ModelName}, {@DateTime}",
                nameof(GetUserByIdAsync),
                nameof(Guid),
                DateTime.UtcNow);

            await getUserQueryValidator.ValidateAndThrowAsync(id, cancellationToken);

            logger.LogDebug(
                "Complited validation {@ValidationName}, {@ModelName}, {@DateTime}",
                nameof(GetUserByIdAsync),
                nameof(Guid),
                DateTime.UtcNow);

            return await usersService.GetUserByIdAsync(id, cancellationToken);
        }

        public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
        {
            var updateUserModelValidator = usersValidator.GetUpdateUserModelValidatorOrThrow();

            logger.LogDebug(
                "Processing validation {@ValidationName}, {@ModelName}, {@DateTime}", 
                nameof(UpdateUserAsync),
                nameof(UpdateUserModel),
                DateTime.UtcNow);

            await updateUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);

            logger.LogDebug(
                "Complited validation {@ValidationName}, {@ModelName}, {@DateTime}",
                nameof(UpdateUserAsync),
                nameof(UpdateUserModel),
                DateTime.UtcNow);

            return await usersService.UpdateUserAsync(user, cancellationToken);
        }
    }
}
