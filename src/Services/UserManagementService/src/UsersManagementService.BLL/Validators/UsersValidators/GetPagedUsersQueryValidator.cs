using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.BLL.Validators.UsersValidators;

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