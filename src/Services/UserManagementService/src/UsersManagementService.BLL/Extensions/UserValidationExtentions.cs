using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Runtime.CompilerServices;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.BLL.Resources;
using UsersManagementService.Common.Exceptions;
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
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredId));
    }

    public static IRuleBuilderOptions<T, string> BaseStringRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(value => !string.IsNullOrWhiteSpace(value))
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredField));
    }

    public static IRuleBuilderOptions<T, string> BaseNamesRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredField))
            .MaximumLength(50)
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationMaxLength));
    }

    public static IRuleBuilderOptions<T, K> BaseNumberRules<T, K>(
        this IRuleBuilder<T, K> ruleBuilder) where K : INumber<K>
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredField))
            .GreaterThanOrEqualTo(K.One)
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationNoPositiveNumber))
            .Must(number =>(number % K.One == K.Zero))
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationNoIntegerNumber));
    }

    public static IRuleBuilderOptions<T, string> BaseEmailRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredField))
            .EmailAddress()
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationInvalidEmail));
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

    public static IRuleBuilderOptions<T, Guid> DoesImageExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder, IImagesRepository imagesRepository)
    {
        return ruleBuilder
            .MustAsync(async (Id, CancellationToken) =>
            {
                return await imagesRepository
                    .DoesImageExistAsync(Id, CancellationToken);
            })
            .WithMessage(resourceHelper.GetValue(UserKeys.ValidationImageDoesntExist));
    }
}