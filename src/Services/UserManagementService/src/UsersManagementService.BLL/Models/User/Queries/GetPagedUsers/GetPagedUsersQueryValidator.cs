using FluentValidation;
using UsersManagementService.BLL.Extensions.ValidatingExtentions;

namespace UsersManagementService.BLL.Models.User.Queries.GetPagedUsers;

public class GetPagedUsersQueryValidator : AbstractValidator<GetPagedUsersQuery>
{
    public GetPagedUsersQueryValidator()
    {
        RuleFor(u => u.PageNumber)
            .CommonNumberRules();
        RuleFor(u => u.PageSize)
            .CommonNumberRules();
    }
}