using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Models.Image;
using static UsersManagementService.BLL.Constants.ValidationRules.ImageValidationRules;

namespace UsersManagementService.BLL.Validators.ImagesValidators;

public class ImageModelValidator : AbstractValidator<ImageModel>
{
    public ImageModelValidator(ImageIdValidator imageIdValidator)
    {
        RuleSet(UpdateImageRules, () =>
        {
            RuleFor(u => u.Id)
                  .MustAsync(async (id, cancellationToken) =>
                  {
                      var result = await imageIdValidator.ValidateAsync(id, cancellationToken);
                      return result.IsValid;
                  });
        });
        RuleSet(CreateImageRules, () =>
        {
            RuleFor(u => u.Id)
                    .BaseIdRules();
        });
        RuleFor(u => u.UserId)
            .BaseIdRules();
        RuleFor(u => u.ImageUrl)
            .BaseStringRules();
    }
}