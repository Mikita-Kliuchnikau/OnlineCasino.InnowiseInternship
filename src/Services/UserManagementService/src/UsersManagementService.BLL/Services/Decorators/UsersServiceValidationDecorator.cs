using FluentValidation;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.BLL.Models.User.UpdateUser;

namespace UsersManagementService.BLL.Services.Decorators
{
    public class UsersServiceValidationDecorator(IUsersService usersService, IUsersValidator usersValidator) : IUsersService
    {
        public async Task<Guid> CreateUserAsync(CreateUserModel user, CancellationToken cancellationToken)
        {
            var createUserModelValidator = usersValidator.CreateUserModelValidator;

            await createUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);

            return await usersService.CreateUserAsync(user, cancellationToken);
        }

        public async Task<Guid> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var deleteUserModelValidator = usersValidator.DeleteUserValidator;

            await deleteUserModelValidator.ValidateAndThrowAsync(id, cancellationToken);

            return await usersService.DeleteUserAsync(id, cancellationToken);
        }

        public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken)
        {
            var getPagedUsersQueryValidator = usersValidator.GetPagedUsersQueryValidator;

            await getPagedUsersQueryValidator.ValidateAndThrowAsync(users, cancellationToken);

            return await usersService.GetPagedUsersAsync(users, cancellationToken);
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var getUserQueryValidator = usersValidator.GetUserValidator;

            await getUserQueryValidator.ValidateAndThrowAsync(id, cancellationToken);

            return await usersService.GetUserByIdAsync(id, cancellationToken);
        }

        public async Task<Guid> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken)
        {
            var updateUserModelValidator = usersValidator.UpdateUserModelValidator;

            await updateUserModelValidator.ValidateAndThrowAsync(user, cancellationToken);

            return await usersService.UpdateUserAsync(user, cancellationToken);
        }
    }
}
