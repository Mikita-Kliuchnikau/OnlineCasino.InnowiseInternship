using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.DeleteUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.BLL.Models.User.UpdateUser;

namespace UsersManagementService.BLL.Models.User;

public class UsersValidator(
    IValidator<CreateUserCommand> createUserCommandValidator,
    IValidator<DeleteUserCommand> deleteUserCommandValidator,
    IValidator<UpdateUserCommand> updateUserCommandValidator,
    IValidator<GetUserQuery> getUserQueryValidator,
    IValidator<GetPagedUsersQuery> getPagedUsersQueryValidator) : IUsersValidator 
{
    public IValidator<CreateUserCommand> CreateUserCommandValidator { get; init; } = createUserCommandValidator;
    public IValidator<DeleteUserCommand> DeleteUserCommandValidator { get; init; } = deleteUserCommandValidator;
    public IValidator<UpdateUserCommand> UpdateUserCommandValidator { get; init; } = updateUserCommandValidator;
    public IValidator<GetUserQuery> GetUserQueryValidatorValidator { get; init; } = getUserQueryValidator;
    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidator { get; init; } = getPagedUsersQueryValidator;
}