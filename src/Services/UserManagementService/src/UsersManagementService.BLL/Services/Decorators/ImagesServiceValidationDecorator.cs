using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Serilog.Context;
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
            "Processing validation {@ValidationName}, {@ModelName}, {@DateTime}",
            nameof(CreateImageAsync),
            nameof(ImageModel),
            DateTime.UtcNow);

        await createImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);

        logger.LogDebug(
            "Complited validation {@ValidationName}, {@ModelName}, {@DateTime}",
            nameof(CreateImageAsync),
            nameof(ImageModel),
            DateTime.UtcNow);

        return await imagesService.CreateImageAsync(image, cancellationToken);
    }

    public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleteImageModelValidator = imagesValidator.GetImageIdValidatorOrThrow();
        logger.LogDebug(
            "Processing validation { @ValidationName}, { @ModelName}, { @DateTime}",
            nameof(DeleteImageAsync),
            nameof(Guid),
            DateTime.UtcNow);

        await deleteImageModelValidator.ValidateAndThrowAsync(id, cancellationToken);

        logger.LogDebug(
            "Complited validation {@ValidationName}, {@ModelName}, {@DateTime}",
            nameof(DeleteImageAsync),
            nameof(Guid),
            DateTime.UtcNow);

        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
    {
        var updateImageModelValidator = imagesValidator.GetImageModelValidatorOrThrow();

        logger.LogDebug(
            "Processing validation { @ValidationName}, { @ModelName}, { @DateTime}",
            nameof(UpdateImageAsync),
            nameof(ImageModel),
            DateTime.UtcNow);

        await updateImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);

        logger.LogDebug(
            "Complited validation {@ValidationName}, {@ModelName}, {@DateTime}",
            nameof(UpdateImageAsync),
            nameof(ImageModel),
            DateTime.UtcNow);

        return await imagesService.UpdateImageAsync(image, cancellationToken);
    }
}
