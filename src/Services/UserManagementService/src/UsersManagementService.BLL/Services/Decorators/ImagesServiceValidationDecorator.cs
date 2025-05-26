using FluentValidation;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.Common.Exceptions;
using static UsersManagementService.BLL.Constants.ValidationRules.ImageValidationRules;

namespace UsersManagementService.BLL.Services.Decorators;

public class ImagesServiceValidationDecorator(IImagesService imagesService, IImagesValidator imagesValidator) : IImagesService
{
    public async Task<Guid> CreateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var createImageModelValidator = imagesValidator.ImageModelValidator;

        if (createImageModelValidator is null)
        {
            throw new NotFoundException(nameof(createImageModelValidator), null!);
        }

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

        if (deleteImageModelValidator is null)
        {
            throw new NotFoundException(nameof(deleteImageModelValidator), null!);
        }

        await deleteImageModelValidator.ValidateAndThrowAsync(id, cancellationToken);

        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var updateImageModelValidator = imagesValidator.ImageModelValidator;

        if (updateImageModelValidator is null)
        {
            throw new NotFoundException(nameof(updateImageModelValidator), null!);
        }

        await updateImageModelValidator.ValidateAsync(image, options =>
        {
            options.IncludeRuleSets(UpdateImageRules);
            options.ThrowOnFailures();
        }, cancellationToken);

        return await imagesService.UpdateImageAsync(image, cancellationToken);
    }
}
