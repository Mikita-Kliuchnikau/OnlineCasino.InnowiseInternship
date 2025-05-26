using FluentValidation;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image;
using static UsersManagementService.BLL.Constants.ValidationRules.ImageValidationRules;

namespace UsersManagementService.BLL.Services.Decorators;

public class ImagesServiceValidationDecorator(IImagesService imagesService, IImagesValidator imagesValidator) : IImagesService
{
    public async Task<Guid> CreateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var createImageModelValidator = imagesValidator.ImageModelValidator;

        await createImageModelValidator.ValidateAsync(image, options =>
        {
            options.IncludeRuleSets(CreateImageRules);
            options.ThrowOnFailures();
        }, cancellationToken);

        return await imagesService.CreateImageAsync(image, cancellationToken);
    }

    public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleteImageModelValidator = imagesValidator.ImageIdValidator;

        await deleteImageModelValidator.ValidateAndThrowAsync(id, cancellationToken);

        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var updateImageModelValidator = imagesValidator.ImageModelValidator;

        await updateImageModelValidator.ValidateAsync(image, options =>
        {
            options.IncludeRuleSets(UpdateImageRules);
            options.ThrowOnFailures();
        }, cancellationToken);

        return await imagesService.UpdateImageAsync(image, cancellationToken);
    }
}
