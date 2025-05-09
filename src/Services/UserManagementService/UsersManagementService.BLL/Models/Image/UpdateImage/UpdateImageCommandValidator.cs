using FluentValidation;
using UsersManagementService.BLL.Constants;
using UsersManagementService.BLL.Extensions.ValidatingExtentions;

namespace UsersManagementService.BLL.Models.Image.UpdateImage;

public class UpdateImageCommandValidator : AbstractValidator<UpdateImageCommand>
{
    public UpdateImageCommandValidator()
    {
        RuleFor(u => u.Id)
            .CommonIdRules();
        RuleFor(u => u.UserId)
            .CommonIdRules();
        RuleFor(u => u.ImageUrl)
            .CommonStringRules();
    }
}