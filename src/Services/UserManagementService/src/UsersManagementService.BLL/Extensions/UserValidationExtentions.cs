using FluentValidation;
using System.Numerics;
using UsersManagementService.BLL.Resources;
using UsersManagementService.Common.Helpers;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Extensions;

public static class UserValidationExtentions
{
    private static readonly ResourceHelper<UserMessages> resourceHelper = new(Common.Enums.CulturePreference.English);

    public static IRuleBuilderOptions<T, Guid> BaseIdRules<T>(
        this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEqual(Guid.Empty)
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredIdMessage));
    }

    public static IRuleBuilderOptions<T, string> BaseStringRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredFieldMessage));
    }

    public static IRuleBuilderOptions<T, string> BaseNamesRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredFieldMessage))
            .MaximumLength(50)
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationMaxLengthMessage));
    }

    public static IRuleBuilderOptions<T, K> BaseNumberRules<T, K>(
        this IRuleBuilder<T, K> ruleBuilder) where K : INumber<K>
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredFieldMessage));
    }

    public static IRuleBuilderOptions<T, string> BaseEmailRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredFieldMessage))
            .EmailAddress()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationInvalidEmailMessage));
    }

    public static IRuleBuilderOptions<T, Guid> DoesUserExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder, IUsersRepository usersRepository)
    {
        return ruleBuilder
            .MustAsync(async (Id, CancellationToken) =>
            {
                return await usersRepository
                    .DoesUserExistAsync(Id, CancellationToken);
            })
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationUserDoesntExist));
    }
}