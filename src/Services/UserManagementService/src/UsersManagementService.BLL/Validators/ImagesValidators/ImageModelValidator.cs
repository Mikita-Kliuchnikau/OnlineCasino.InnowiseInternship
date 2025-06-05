using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.BLL.Resources;
using UsersManagementService.Common.Helpers;
using UsersManagementService.DAL.Interfaces.Repositories;
using static UsersManagementService.BLL.Constants.ValidationRules.ImageValidationRules;

namespace UsersManagementService.BLL.Validators.ImagesValidators;

public class ImageModelValidator : AbstractValidator<ImageModel>
{
    private static readonly ResourceHelper<UserMessages> resourceHelper = new(Common.Enums.CulturePreference.English);

    public ImageModelValidator(ImageIdValidator imageIdValidator, IImagesRepository imagesRepository)
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
            RuleFor(u => u)
            .MustAsync(async (image, cancellationToken) =>
            {
                return await imagesRepository.IsImageUniqeAsync(
                    id: image.Id,
                    cancellationToken: cancellationToken);
            }).WithMessage(resourceHelper.GetValue(UserKeys.ValidationNotUniqueImage));
        });
        RuleFor(u => u.UserId)
            .BaseIdRules();
    }
}