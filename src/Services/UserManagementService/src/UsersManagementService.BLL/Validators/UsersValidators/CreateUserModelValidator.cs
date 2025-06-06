using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.BLL.Validators.UsersValidators;

public class CreateUserModelValidator : AbstractValidator <CreateUserModel>
{
    public CreateUserModelValidator()
    {
        RuleFor(u => u.AuthId)
            .BaseIdRules();
        RuleFor(u => u.Username)
            .BaseNamesRules();
        RuleFor(u => u.Email)
            .BaseEmailRules();
    }
}