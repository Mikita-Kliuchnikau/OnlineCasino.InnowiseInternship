using FluentValidation;
using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.BLL.Interfaces.Validators;
public interface IUsersValidator 
{
    public IValidator<CreateUserModel> GetCreateUserModelValidatorOrThrow();
    public IValidator<Guid> GetUserIdValidatorOrThrow();
    public IValidator<UpdateUserModel> GetUpdateUserModelValidatorOrThrow();
    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidatorOrThrow();
}