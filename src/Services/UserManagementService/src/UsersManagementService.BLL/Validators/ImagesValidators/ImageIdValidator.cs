using FluentValidation;
using UsersManagementService.BLL.Extensions;

namespace UsersManagementService.BLL.Validators.ImagesValidators;

public class ImageIdValidator : AbstractValidator<Guid>
{
    public ImageIdValidator()
    {
        RuleFor(id => id)
            .BaseIdRules();
    }
}