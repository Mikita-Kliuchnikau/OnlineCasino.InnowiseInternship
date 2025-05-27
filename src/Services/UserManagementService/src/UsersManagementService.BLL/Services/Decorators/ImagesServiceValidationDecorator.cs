using FluentValidation;
using Microsoft.Extensions.Logging;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using static UsersManagementService.Common.Constants.LoggingMessages;
using UsersManagementService.BLL.Models.Image;
using static UsersManagementService.BLL.Constants.ValidationRules.ImageValidationRules;

namespace UsersManagementService.BLL.Services.Decorators;

public class ImagesServiceValidationDecorator(
    IImagesService imagesService, 
    IImagesValidator imagesValidator,
    ILogger<ImagesServiceValidationDecorator> logger) : IImagesService
{
    public async Task<Guid> CreateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var createImageModelValidator = imagesValidator.GetImageModelValidatorOrThrow();

        await createImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);

        logger.LogDebug(string.Format(
            ValidatingSucceededMessage,
            nameof(CreateImageAsync),
            nameof(CreateImageModel),
            DateTime.UtcNow));

        return await imagesService.CreateImageAsync(image, cancellationToken);
    }

    public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleteImageModelValidator = imagesValidator.GetImageIdValidatorOrThrow();

        logger.LogDebug(string.Format(
            ValidatingStartingMessage,
            nameof(DeleteImageAsync),
            nameof(Guid),
            DateTime.UtcNow));

        try
        {
            await deleteImageModelValidator.ValidateAndThrowAsync(id, cancellationToken);
        }
        catch (Exception ex) 
        {
            logger.LogError(string.Format(
                ValidatingFailedMessage,
                nameof(DeleteImageAsync),
                nameof(Guid),
                ex.Message,
                DateTime.UtcNow));
            throw;
        }

        logger.LogDebug(string.Format(
            ValidatingSucceededMessage,
            nameof(DeleteImageAsync),
            nameof(Guid),
            DateTime.UtcNow));

        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var updateImageModelValidator = imagesValidator.GetImageModelValidatorOrThrow();

        await updateImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);

        return await imagesService.UpdateImageAsync(image, cancellationToken);
    }
}
