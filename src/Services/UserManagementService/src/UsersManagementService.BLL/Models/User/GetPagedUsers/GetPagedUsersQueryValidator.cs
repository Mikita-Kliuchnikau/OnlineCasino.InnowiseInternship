using FluentValidation;
using UsersManagementService.BLL.Extensions;

namespace UsersManagementService.BLL.Models.User.GetPagedUsers;

public class GetPagedUsersQueryValidator : AbstractValidator<GetPagedUsersQuery>
{
    public GetPagedUsersQueryValidator()
    {
        RuleFor(u => u.PageNumber)
            .BaseNumberRules();
        RuleFor(u => u.PageSize)
            .BaseNumberRules();
    }
}