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
        get
        { 
            var validator = _createUserModelValidator.FirstOrDefault(v => v is CreateUserModelValidator);
            return validator;
        }
    }
    public IValidator<Guid>? UserIdValidator 
    { 
        get
        {
            var validator = _userIdValidator.FirstOrDefault(v => v is UserIdValidator);
            return validator;
        }
    }

    public IValidator<UpdateUserModel>? UpdateUserModelValidator 
    { 
        get
        {
            var validator = _updateUserModelValidator.FirstOrDefault(v => v is UpdateUserModelValidator);
            return validator;
        }
    }
    public IValidator<GetPagedUsersQuery>? GetPagedUsersQueryValidator 
    { 
        get
        {
            var validator = _getPagedUsersQueryValidator.FirstOrDefault(v => v is GetPagedUsersQueryValidator);
            return validator;
        }
    }
}