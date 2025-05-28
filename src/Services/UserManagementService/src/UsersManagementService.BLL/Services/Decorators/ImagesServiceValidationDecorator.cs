using FluentValidation;
using Microsoft.Extensions.Logging;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image;
using static UsersManagementService.Common.Constants.LoggingMessages;

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
            ValidatingStartingMessage,
            nameof(CreateImageAsync),
            nameof(ImageModel),
            DateTime.UtcNow);

        try
        {
            await createImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);
        }
        catch (ValidationException ex)
        {
            logger.LogError(
                ValidatingFailedMessage,
                nameof(CreateImageAsync),
                nameof(ImageModel),
                ex.Message,
                DateTime.UtcNow);
            throw;
        }

        logger.LogDebug(
            ValidatingSucceededMessage,
            nameof(CreateImageAsync),
            nameof(ImageModel),
            DateTime.UtcNow);

        return await imagesService.CreateImageAsync(image, cancellationToken);
    }

    public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleteImageModelValidator = imagesValidator.GetImageIdValidatorOrThrow();

        logger.LogDebug(
            ValidatingStartingMessage,
            nameof(DeleteImageAsync),
            nameof(Guid),
            DateTime.UtcNow);

        try
        {
            await deleteImageModelValidator.ValidateAndThrowAsync(id, cancellationToken);
        }
        catch (ValidationException ex) 
        {
            logger.LogError(
                ValidatingFailedMessage,
                nameof(DeleteImageAsync),
                nameof(Guid),
                ex.Message,
                DateTime.UtcNow);
            throw;
        }

        logger.LogDebug(
            ValidatingSucceededMessage,
            nameof(DeleteImageAsync),
            nameof(Guid),
            DateTime.UtcNow);

        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var updateImageModelValidator = imagesValidator.GetImageModelValidatorOrThrow();

        logger.LogDebug(
            ValidatingStartingMessage,
            nameof(UpdateImageAsync),
            nameof(ImageModel),
            DateTime.UtcNow);

        try
        {
            await updateImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);
        }
        catch (ValidationException ex)
        {
            logger.LogError(
                ValidatingFailedMessage,
                nameof(UpdateImageAsync),
                nameof(ImageModel),
                ex.Message,
                DateTime.UtcNow);
            throw;
        }

        logger.LogDebug(
            ValidatingSucceededMessage,
            nameof(UpdateImageAsync),
            nameof(ImageModel),
            DateTime.UtcNow);

        return await imagesService.UpdateImageAsync(image, cancellationToken);
    }
}
