using FluentValidation;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.DeleteUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.BLL.Models.User.UpdateUser;

namespace UsersManagementService.BLL.Services.Decorators
{
    public class UsersServiceValidationDecorator(IUsersService usersService, IUsersValidator usersValidator) : IUsersService
    {
        public async Task<Guid> CreateUserAsync(CreateUserCommand user, CancellationToken cancellationToken)
        {
            var createUserCommandValidator = usersValidator.CreateUserCommandValidator;
            
            await createUserCommandValidator.ValidateAndThrowAsync(user, cancellationToken);

            return await usersService.CreateUserAsync(user, cancellationToken);
        }

        public async Task<Guid> DeleteUserAsync(DeleteUserCommand user, CancellationToken cancellationToken)
        {
            var deleteUserCommandValidator = usersValidator.DeleteUserCommandValidator;

            await deleteUserCommandValidator.ValidateAndThrowAsync(user, cancellationToken);

            return await usersService.DeleteUserAsync(user, cancellationToken);
        }

        public async Task<PagedUsersViewModel> GetPagedUsersAsync(GetPagedUsersQuery users, CancellationToken cancellationToken)
        {
            var getPagedUsersQueryValidator = usersValidator.GetPagedUsersQueryValidator;

            await getPagedUsersQueryValidator.ValidateAndThrowAsync(users, cancellationToken);

            return await usersService.GetPagedUsersAsync(users, cancellationToken);
        }

        public async Task<UserViewModel> GetUserByIdAsync(GetUserQuery user, CancellationToken cancellationToken)
        {
            var getUserQueryValidator = usersValidator.GetUserQueryValidatorValidator;

            await getUserQueryValidator.ValidateAndThrowAsync(user, cancellationToken);

            return await usersService.GetUserByIdAsync(user, cancellationToken);
        }

        public async Task<Guid> UpdateUserAsync(UpdateUserCommand user, CancellationToken cancellationToken)
        {
            var updateUserCommandValidator = usersValidator.UpdateUserCommandValidator;

            await updateUserCommandValidator.ValidateAndThrowAsync(user, cancellationToken);

            return await usersService.UpdateUserAsync(user, cancellationToken);
        }
    }
}
