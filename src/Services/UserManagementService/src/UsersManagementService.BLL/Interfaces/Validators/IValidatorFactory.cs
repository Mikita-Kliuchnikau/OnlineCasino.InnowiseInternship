using FluentValidation;

namespace UsersManagementService.BLL.Interfaces.Validators;

public interface IValidatorFactory
{
    public T GetValidatorOrThrow<T>()  where T : IValidator;
}
