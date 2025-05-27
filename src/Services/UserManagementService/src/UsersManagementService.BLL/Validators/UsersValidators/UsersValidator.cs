using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Exceptions;

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

    public IValidator<CreateUserModel> GetCreateUserModelValidatorOrThrow()
    {
        var validator = _createUserModelValidator.FirstOrDefault(v => v is CreateUserModelValidator);

        return validator is null ? throw new NotFoundException(nameof(createUserModelValidator), null!) : validator;
    }
    public IValidator<Guid> GetUserIdValidatorOrThrow()
    {
        var validator = _userIdValidator.FirstOrDefault(v => v is UserIdValidator);

        return validator is null ? throw new NotFoundException(nameof(userIdValidator), null!) : validator;
    }

    public IValidator<UpdateUserModel> GetUpdateUserModelValidatorOrThrow()
    {
        var validator = _updateUserModelValidator.FirstOrDefault(v => v is UpdateUserModelValidator);
        return validator is null ? throw new NotFoundException(nameof(updateUserModelValidator), null!) : validator;
    } 

    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidatorOrThrow()
    {
        var validator = _getPagedUsersQueryValidator.FirstOrDefault(v => v is GetPagedUsersQueryValidator);
        return validator is null ? throw new NotFoundException(nameof(getPagedUsersQueryValidator), null!) : validator;
    }
}