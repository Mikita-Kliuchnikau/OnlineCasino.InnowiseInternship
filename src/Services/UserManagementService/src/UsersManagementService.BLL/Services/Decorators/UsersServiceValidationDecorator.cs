using FluentValidation;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Exceptions;

namespace UsersManagementService.BLL.Services.Decorators
{
    public class UsersServiceValidationDecorator(IUsersService usersService, IUsersValidator usersValidator) : IUsersService
    {
        public async Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken = default)
        {
            var createUserModelValidator = usersValidator.CreateUserModelValidator;

            if (createUserModelValidator is null)
            {
                throw new NotFoundException(nameof(createUserModelValidator), null!);
            }

            await createUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);

            return await usersService.CreateUserAsync(user, cancellationToken);
        }

        public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var deleteUserModelValidator = usersValidator.UserIdValidator;

            if (deleteUserModelValidator is null)
            {
                throw new NotFoundException(nameof(deleteUserModelValidator), null!);
            }

            await deleteUserModelValidator.ValidateAndThrowAsync(id, cancellationToken);

            return await usersService.DeleteUserAsync(id, cancellationToken);
        }

        public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken = default)
        {
            var getPagedUsersQueryValidator = usersValidator.GetPagedUsersQueryValidator;

            if (getPagedUsersQueryValidator is null)
            {
                throw new NotFoundException(nameof(getPagedUsersQueryValidator), null!);
            }

            await getPagedUsersQueryValidator.ValidateAndThrowAsync(users, cancellationToken);

            return await usersService.GetPagedUsersAsync(users, cancellationToken);
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var getUserQueryValidator = usersValidator.UserIdValidator;

            if (getUserQueryValidator is null)
            {
                throw new NotFoundException(nameof(getUserQueryValidator), null!);
            }

            await getUserQueryValidator.ValidateAndThrowAsync(id, cancellationToken);

            return await usersService.GetUserByIdAsync(id, cancellationToken);
        }

        public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
        {
            var updateUserModelValidator = usersValidator.UpdateUserModelValidator;
            
            if (updateUserModelValidator is null)
            {
                throw new NotFoundException(nameof(updateUserModelValidator), null!);
            }

            await updateUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);

            return await usersService.UpdateUserAsync(user, cancellationToken);
        }
    }
}
