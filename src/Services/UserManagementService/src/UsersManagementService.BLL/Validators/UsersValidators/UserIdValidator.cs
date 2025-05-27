using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Validators.UsersValidators;

public class UserIdValidator : AbstractValidator<Guid>
{
    public UserIdValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u)
            .DoesUserExist(usersRepository)
            .BaseIdRules();
    }
}