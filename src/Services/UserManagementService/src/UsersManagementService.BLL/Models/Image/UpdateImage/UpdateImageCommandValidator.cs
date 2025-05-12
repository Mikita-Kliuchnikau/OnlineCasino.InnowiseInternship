using FluentValidation;
using UsersManagementService.BLL.Extensions;

namespace UsersManagementService.BLL.Models.Image.UpdateImage;

public class UpdateImageCommandValidator : AbstractValidator<UpdateImageCommand>
{
    public UpdateImageCommandValidator()
    {
        RuleFor(u => u.Id)
            .BaseIdRules();
        RuleFor(u => u.UserId)
            .BaseIdRules();
        RuleFor(u => u.ImageUrl)
            .BaseStringRules();
    }
}