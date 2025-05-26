using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.BLL.Validators.UsersValidators;

public class UsersValidator(
        IEnumerable<IValidator<CreateUserModel>> createUserModelValidator,
        IEnumerable<IValidator<Guid>> userIdValidator,
        IEnumerable<IValidator<UpdateUserModel>> updateUserModelValidator,
        IEnumerable<IValidator<GetPagedUsersQuery>> getPagedUsersQueryValidator) : IUsersValidator 
{
    private readonly IEnumerable<IValidator<CreateUserModel>> _createUserModelValidator = createUserModelValidator;
    private readonly IEnumerable<IValidator<Guid>> _userIdValidator = userIdValidator;
    private readonly IEnumerable<IValidator<UpdateUserModel>> _updateUserModelValidator = updateUserModelValidator;
    private readonly IEnumerable<IValidator<GetPagedUsersQuery>> _getPagedUsersQueryValidator = getPagedUsersQueryValidator;

    public IValidator<CreateUserModel>? CreateUserModelValidator 
    {
        get => _createUserModelValidator.FirstOrDefault(v => v is CreateUserModelValidator);

    }
    public IValidator<Guid>? UserIdValidator 
    {
        get => _userIdValidator.FirstOrDefault(v => v is UserIdValidator);
    }

    public IValidator<UpdateUserModel>? UpdateUserModelValidator 
    {
        get => _updateUserModelValidator.FirstOrDefault(v => v is UpdateUserModelValidator);

    }
    public IValidator<GetPagedUsersQuery>? GetPagedUsersQueryValidator 
    {
        get => _getPagedUsersQueryValidator.FirstOrDefault(v => v is GetPagedUsersQueryValidator);
    }
}