using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.User.DeleteUser;

public class DeleteUserValidator : AbstractValidator<Guid>
{
    public DeleteUserValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u)
            .DoesUserExist(usersRepository)
            .BaseIdRules();
    }
}