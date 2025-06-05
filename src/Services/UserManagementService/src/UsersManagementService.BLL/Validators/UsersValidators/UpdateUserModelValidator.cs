using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.BLL.Validators.UsersValidators;

public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    public UpdateUserModelValidator()
    {
        RuleFor(u => u.Id)
            .BaseIdRules();
        RuleFor(u => u.AuthId)
            .BaseIdRules();
        RuleFor(u => u.Username)
            .BaseNamesRules();
        RuleFor(u => u.Email)
            .BaseEmailRules();
        RuleFor(u => u.FirstName!)
            .BaseNamesRules()
            .When(u => u.FirstName is not null);
        RuleFor(u => u.SecondName!)
            .BaseNamesRules()
            .When(u => u.SecondName is not null);
        RuleFor(u => u.LastName!)
            .BaseNamesRules()
            .When(u => u.LastName is not null);
        RuleFor(u => u.PassportNumber!)
            .BaseStringRules()
            .When(u => u.PassportNumber is not null);
        RuleFor(u => u.IdentificationNumber!)
            .BaseStringRules()
            .When(u => u.IdentificationNumber is not null);
    }
}