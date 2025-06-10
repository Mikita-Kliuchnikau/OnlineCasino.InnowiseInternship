using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.BLL.Resources;
using UsersManagementService.Common.Helpers;
using UsersManagementService.DAL.Interfaces.Repositories;
using static UsersManagementService.BLL.Constants.ValidationError;

namespace UsersManagementService.BLL.Validators.UsersValidators;

public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    private static readonly ResourceHelper<UserMessages> resourceHelper = new(Common.Enums.CulturePreference.English);

    public CreateUserModelValidator(IUsersRepository repository)
    {
        RuleFor(u => u.AuthId)
            .BaseIdRules();
        RuleFor(u => u.Username)
            .BaseNamesRules();
        RuleFor(u => u.Email)
            .BaseEmailRules();
        RuleFor(u => u)
            .MustAsync(async (model, cancellationToken) =>
            {
                return await repository.IsUserUniqueAsync(model.AuthId, model.Username, model.Email, cancellationToken);
            }).WithMessage(string.IsNullOrEmpty(resourceHelper.GetValue(UserKeys.ValidationNotUniqueUser)) ? MessageNotFound : resourceHelper.GetValue(UserKeys.ValidationNotUniqueUser));
    }
}
