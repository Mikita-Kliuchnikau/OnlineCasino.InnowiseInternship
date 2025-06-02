using FluentValidation;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.BLL.Validators.ImagesValidators;
using UsersManagementService.BLL.Validators.UsersValidators;
using UsersManagementService.Common.Exceptions;
using IValidatorFactory = UsersManagementService.BLL.Interfaces.Validators.IValidatorFactory;

namespace UsersManagementService.BLL.Validators;

public class ValidatorFactory(
    IEnumerable<IValidator<Guid>> IdValidators,
    IEnumerable<IValidator<ImageModel>> imageModelValidators,
    IEnumerable<IValidator<CreateUserModel>> createUserModelValidators,
    IEnumerable<IValidator<UpdateUserModel>> updateUserModelValidators,
    IEnumerable<IValidator<GetPagedUsersQuery>> getPagedUsersQueryValidators)
        : IValidatorFactory
{
    private readonly IEnumerable<IValidator<Guid>> _idValidators = IdValidators;
    private readonly IEnumerable<IValidator<ImageModel>> _imageModelValidators = imageModelValidators;
    private readonly IEnumerable<IValidator<CreateUserModel>> _createUserModelValidators = createUserModelValidators;
    private readonly IEnumerable<IValidator<UpdateUserModel>> _updateUserModelValidators = updateUserModelValidators;
    private readonly IEnumerable<IValidator<GetPagedUsersQuery>> _getPagedUsersQueryValidators = getPagedUsersQueryValidators;

    public T GetValidatorOrThrow<T>() where T : IValidator
    {
        return typeof(T).Name switch
        {
            nameof(ImageIdValidator) => (T)GetImageIdValidatorOrThrow(),
            nameof(ImageModelValidator) => (T)GetImageModelValidatorOrThrow(),
            nameof(UserIdValidator) => (T)GetUserIdValidatorOrThrow(),
            nameof(CreateUserModelValidator) => (T)GetCreateUserModelValidatorOrThrow(),
            nameof(UpdateUserModelValidator) => (T)GetUpdateUserModelValidatorOrThrow(),
            nameof(GetPagedUsersQueryValidator) => (T)GetPagedUsersQueryValidatorOrThrow(),
            _ => throw new NotFoundException(nameof(T), null!)
        };
    }

    private IValidator<Guid> GetImageIdValidatorOrThrow()
    {
        var validator = _idValidators.FirstOrDefault(v => v is ImageIdValidator);
        return validator ?? throw new NotFoundException(nameof(ImageIdValidator), null!);
    }

    private IValidator<ImageModel> GetImageModelValidatorOrThrow()
    {
        var validator = _imageModelValidators.FirstOrDefault(v => v is ImageModelValidator);

        return validator ?? throw new NotFoundException(nameof(ImageModelValidator), null!);
    }

    private IValidator<Guid> GetUserIdValidatorOrThrow()
    {
        var validator = _idValidators.FirstOrDefault(v => v is UserIdValidator);

        return validator ?? throw new NotFoundException(nameof(UserIdValidator), null!);
    }

    private IValidator<CreateUserModel> GetCreateUserModelValidatorOrThrow()
    {
        var validator = _createUserModelValidators.FirstOrDefault(v => v is CreateUserModelValidator);

        return validator ?? throw new NotFoundException(nameof(CreateUserModelValidator), null!);

    }

    private IValidator<UpdateUserModel> GetUpdateUserModelValidatorOrThrow()
    {
        var validator = _updateUserModelValidators.FirstOrDefault(v => v is UpdateUserModelValidator);
        return validator ?? throw new NotFoundException(nameof(UpdateUserModelValidator), null!);
    }

    private IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidatorOrThrow()
    {
        var validator = _getPagedUsersQueryValidators.FirstOrDefault(v => v is GetPagedUsersQueryValidator);
        return validator ?? throw new NotFoundException(nameof(GetPagedUsersQueryValidator), null!);
    }
}
