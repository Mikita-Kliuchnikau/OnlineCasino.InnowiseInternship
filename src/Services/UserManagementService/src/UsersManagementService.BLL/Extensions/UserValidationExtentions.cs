using FluentValidation;
using System.Numerics;
using UsersManagementService.BLL.Resources;
using UsersManagementService.Common.Helpers;

namespace UsersManagementService.BLL.Extensions;

public static class UserValidationExtentions
{
    private static readonly ResourceHelper<UserMessages> resourceHelper = new(Common.Enums.CulturePreference.English);

    public static IRuleBuilderOptions<T, Guid> BaseIdRules<T>(
        this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEqual(Guid.Empty)
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredId) ?? "Error message not found");
    }

    public static IRuleBuilderOptions<T, string> BaseStringRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(value => !string.IsNullOrWhiteSpace(value))
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredField) ?? "Error message not found");
    }

    public static IRuleBuilderOptions<T, string> BaseNamesRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredField) ?? "Error message not found")
            .MaximumLength(50)
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationMaxLength) ?? "Error message not found");
    }

    public static IRuleBuilderOptions<T, K> BaseNumberRules<T, K>(
        this IRuleBuilder<T, K> ruleBuilder) where K : INumber<K>
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredField) ?? "Error message not found")
            .GreaterThanOrEqualTo(K.One)
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationNoPositiveNumber) ?? "Error message not found")
            .Must(number =>(number % K.One == K.Zero))
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationNoIntegerNumber) ?? "Error message not found");
    }

    public static IRuleBuilderOptions<T, string> BaseEmailRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredField) ?? "Error message not found")
            .EmailAddress()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationInvalidEmail) ?? "Error message not found");
    }
}