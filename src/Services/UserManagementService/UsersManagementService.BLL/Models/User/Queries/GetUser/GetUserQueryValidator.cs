using FluentValidation;
using UsersManagementService.BLL.Extensions.ValidatingExtentions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.User.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u.Id)
            .DoesUserExist(usersRepository)
            .CommonIdRules();
    }
}