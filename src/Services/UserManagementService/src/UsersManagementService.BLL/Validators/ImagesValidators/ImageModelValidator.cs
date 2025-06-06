using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Models.Image;

namespace UsersManagementService.BLL.Validators.ImagesValidators;

public class ImageModelValidator : AbstractValidator<ImageModel>
{
    public ImageModelValidator()
    {
        RuleFor(u => u.Id)
            .BaseIdRules();
        RuleFor(u => u.UserId)
            .BaseIdRules();
    }
}