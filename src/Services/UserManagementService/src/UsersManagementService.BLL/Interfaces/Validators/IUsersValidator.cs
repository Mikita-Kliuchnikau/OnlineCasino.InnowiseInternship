using FluentValidation;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.DeleteUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.BLL.Models.User.UpdateUser;

namespace UsersManagementService.BLL.Interfaces.Validators;
public interface IUsersValidator 
{
    public IValidator<CreateUserCommand> CreateUserCommandValidator { get; init; }
    public IValidator<DeleteUserCommand> DeleteUserCommandValidator { get; init; }
    public IValidator<UpdateUserCommand> UpdateUserCommandValidator { get; init; }
    public IValidator<GetUserQuery> GetUserQueryValidatorValidator { get; init; }
    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidator { get; init; }
}