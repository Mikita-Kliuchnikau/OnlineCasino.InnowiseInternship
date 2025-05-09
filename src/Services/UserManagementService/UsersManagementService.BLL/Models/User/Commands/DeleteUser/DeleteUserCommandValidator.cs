using FluentValidation;
using UsersManagementService.BLL.Extensions.ValidatingExtentions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.User.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u.Id)
            .DoesUserExist(usersRepository)
            .CommonIdRules();
    }
}