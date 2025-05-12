using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.User.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u.Id)
            .DoesUserExist(usersRepository)
            .BaseIdRules();
    }
}