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
            .WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationRequiredId)) ? "Message not found" : resourceHelper.GetValue(UserKeys.ValidationRequiredId));
    }

    public static IRuleBuilderOptions<T, string> BaseStringRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(value => !string.IsNullOrWhiteSpace(value))
            .WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationRequiredField)) ? "Message not found": resourceHelper.GetValue(UserKeys.ValidationRequiredField));
    }

    public static IRuleBuilderOptions<T, string> BaseNamesRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationRequiredField)) ? "Message not found" : resourceHelper.GetValue(UserKeys.ValidationRequiredField))
            .MaximumLength(50)
            .WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationMaxLength)) ? "Message not found" : resourceHelper.GetValue(UserKeys.ValidationMaxLength));
    }

    public static IRuleBuilderOptions<T, K> BaseNumberRules<T, K>(
        this IRuleBuilder<T, K> ruleBuilder) where K : INumber<K>
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationRequiredField)) ? "Message not found" : resourceHelper.GetValue(UserKeys.ValidationRequiredField))
            .GreaterThanOrEqualTo(K.One)
            .WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationNoPositiveNumber)) ? "Message not found" : resourceHelper.GetValue(UserKeys.ValidationNoPositiveNumber))
            .Must(number =>(number % K.One == K.Zero))
            .WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationNoIntegerNumber)) ? "Message not found" : resourceHelper.GetValue(UserKeys.ValidationNoIntegerNumber));
    }

    public static IRuleBuilderOptions<T, string> BaseEmailRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationRequiredField)) ? "Message not found" : resourceHelper.GetValue(UserKeys.ValidationRequiredField))
            .EmailAddress()
            .WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationInvalidEmail)) ? "Message not found" : resourceHelper.GetValue(UserKeys.ValidationInvalidEmail));
    }
}