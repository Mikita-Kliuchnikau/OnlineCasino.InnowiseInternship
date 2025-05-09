using FluentValidation;
using UsersManagementService.BLL.Extensions.ValidatingExtentions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u.Id)
            .DoesUserExist(usersRepository)
            .CommonIdRules();
        RuleFor(u => u.AuthId)
            .CommonIdRules();
        RuleFor(u => u.Username)
            .CommonNamesRules();
        RuleFor(u => u.Email)
            .CommonEmailRules();
        RuleFor(u => u.FirstName!)
            .CommonNamesRules()
            .When(u => u.FirstName is not null);
        RuleFor(u => u.SecondName!)
            .CommonNamesRules()
            .When(u => u.SecondName is not null);
        RuleFor(u => u.LastName!)
            .CommonNamesRules()
            .When(u => u.LastName is not null);
        RuleFor(u => u.PassportNumber!)
            .CommonStringRules()
            .When(u => u.PassportNumber is not null);
        RuleFor(u => u.IdentificationNumber!)
            .CommonStringRules()
            .When(u => u.IdentificationNumber is not null);
    }
}