using FluentValidation;
using UsersManagementService.BLL.Extensions.ValidatingExtentions;

namespace UsersManagementService.BLL.Models.Image.CreateImage;

public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
{
    public CreateImageCommandValidator()
    {
        RuleFor(u => u.Id)
            .CommonIdRules();
        RuleFor(u => u.UserId)
            .CommonIdRules();
        RuleFor(u => u.ImageUrl)
            .CommonStringRules();
    }
}