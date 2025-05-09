using FluentValidation;
using System.Numerics;
using UsersManagementService.BLL.Constants;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Extensions.ValidatingExtentions;

public static class UserValidationExtentions
{
    public static IRuleBuilderOptions<T, Guid> CommonIdRules<T>(
        this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.ValidationRequiredIdMessage);
    }

    public static IRuleBuilderOptions<T, string> CommonStringRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(ValidationMessages.ValidationRequiredFieldMessage);
    }

    public static IRuleBuilderOptions<T, string> CommonNamesRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(ValidationMessages.ValidationRequiredFieldMessage)
            .MaximumLength(50)
            .WithMessage(ValidationMessages.ValidationMaxLengthMessage);
    }

    public static IRuleBuilderOptions<T, K> CommonNumberRules<T, K>(
        this IRuleBuilder<T, K> ruleBuilder) where K : INumber<K>
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(ValidationMessages.ValidationRequiredFieldMessage);
    }

    public static IRuleBuilderOptions<T, string> CommonEmailRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(ValidationMessages.ValidationRequiredFieldMessage)
            .EmailAddress()
            .WithMessage(ValidationMessages.ValidationInvalidEmailMessage);
    }

    public static IRuleBuilderOptions<T, Guid> DoesUserExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder, IUsersRepository usersRepository)
    {
        return ruleBuilder
            .MustAsync(async (Id, CancellationToken) =>
            {
                return await usersRepository
                .DoesUserExistAsync(
                    Id,
                    cancellationToken: CancellationToken);
            })
            .WithMessage(ValidationMessages.ValidationUserDoesntExist);
    }
}