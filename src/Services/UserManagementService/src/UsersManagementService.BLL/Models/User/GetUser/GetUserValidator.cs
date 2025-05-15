using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.User.GetUser;

public class GetUserValidator : AbstractValidator<Guid>
{
    public GetUserValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u)
            .DoesUserExist(usersRepository)
            .BaseIdRules();
    }
}