using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Exceptions;

namespace UsersManagementService.BLL.Validators.UsersValidators;

public class UsersValidator(
    IValidator<CreateUserModel> createUserModelValidator,
    IValidator<Guid> userIdValidator,
    IValidator<UpdateUserModel> updateUserModelValidator,
    IValidator<GetPagedUsersQuery> getPagedUsersQueryValidator) : IUsersValidator 
{
    private readonly List<IValidator<CreateUserModel>> _createUserModelValidator = [];
    private readonly List<IValidator<Guid>> _userIdValidator = [];
    private readonly List<IValidator<UpdateUserModel>> _updateUserModelValidator = [];
    private readonly List<IValidator<GetPagedUsersQuery>> _getPagedUsersQueryValidator = [];

    public IValidator<CreateUserModel> CreateUserModelValidator 
    {
        get
        { 
            var validator = _createUserModelValidator.FirstOrDefault(v => v is CreateUserModelValidator);
            if (validator is null)
            {
                throw new NotFoundException(nameof(validator), null!);
            }
            return validator;
        }
        set => _createUserModelValidator.Add(createUserModelValidator);
    }
    public IValidator<Guid> UserIdValidator 
    { 
        get
        {
            var validator = _userIdValidator.FirstOrDefault(v => v is UserIdValidator);
            if (validator is null)
            {
                throw new NotFoundException(nameof(validator), null!);
            }
            return validator;
        }
        set => _userIdValidator.Add(userIdValidator);
    }

    public IValidator<UpdateUserModel> UpdateUserModelValidator 
    { 
        get
        {
            var validator = _updateUserModelValidator.FirstOrDefault(v => v is UpdateUserModelValidator);
            if (validator is null)
            {
                throw new NotFoundException(nameof(validator), null!);
            }
            return validator;
        }
        set => _updateUserModelValidator.Add(updateUserModelValidator);
    }
    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidator 
    { 
        get
        {
            var validator = _getPagedUsersQueryValidator.FirstOrDefault(v => v is GetPagedUsersQueryValidator);
            if (validator is null)
            {
                throw new NotFoundException(nameof(validator), null!);
            }
            return validator;
        }
        set => _getPagedUsersQueryValidator.Add(getPagedUsersQueryValidator);
    }
}