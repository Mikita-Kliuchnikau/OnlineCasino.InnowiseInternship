using FluentValidation;
using Microsoft.Extensions.Logging;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image;

namespace UsersManagementService.BLL.Services.Decorators;

public class ImagesServiceValidationDecorator(
    IImagesService imagesService, 
    IImagesValidator imagesValidator,
    ILogger<ImagesServiceValidationDecorator> logger) : IImagesService
{
    public async Task<Guid> CreateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var createImageModelValidator = imagesValidator.GetImageModelValidatorOrThrow();

        logger.LogDebug(
            "Processing validation {ValidationName}, {@Model}",
            nameof(CreateImageAsync),
            image);

        await createImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);

        logger.LogDebug(
            "Complited validation {ValidationName}, {@Model}",
            nameof(CreateImageAsync),
            image);

        return await imagesService.CreateImageAsync(image, cancellationToken);
    }

    public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleteImageModelValidator = imagesValidator.GetImageIdValidatorOrThrow();
        logger.LogDebug(
            "Processing validation {ValidationName}, {@Model}",
            nameof(DeleteImageAsync),
            id);

        await deleteImageModelValidator.ValidateAndThrowAsync(id, cancellationToken);

        logger.LogDebug(
            "Complited validation {ValidationName}, {@Model}",
            nameof(DeleteImageAsync),
            id);

        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var updateImageModelValidator = imagesValidator.GetImageModelValidatorOrThrow();

        logger.LogDebug(
            "Processing validation {ValidationName}, {@Model}",
            nameof(UpdateImageAsync),
            image);

        await updateImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);

        logger.LogDebug(
            "Complited validation {ValidationName}, {@Model}",
            nameof(UpdateImageAsync),
            image);

        return await imagesService.UpdateImageAsync(image, cancellationToken);
    }
}
