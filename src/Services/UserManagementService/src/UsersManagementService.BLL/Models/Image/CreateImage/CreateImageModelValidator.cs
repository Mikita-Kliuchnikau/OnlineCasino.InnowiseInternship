using FluentValidation;
using UsersManagementService.BLL.Extensions;

namespace UsersManagementService.BLL.Models.Image.CreateImage;

public class CreateImageModelValidator : AbstractValidator<CreateImageModel>
{
    public CreateImageModelValidator()
    {
        RuleFor(u => u.Id)
            .BaseIdRules();
        RuleFor(u => u.UserId)
            .BaseIdRules();
        RuleFor(u => u.ImageUrl)
            .BaseStringRules();
    }
}