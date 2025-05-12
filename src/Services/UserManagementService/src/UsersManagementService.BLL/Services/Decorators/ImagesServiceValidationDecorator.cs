using FluentValidation;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;

namespace UsersManagementService.BLL.Services.Decorators;

public class ImagesServiceValidationDecorator(IImagesService imagesService, IImagesValidator imagesValidator) : IImagesService
{
    public async Task<Guid> CreateImageAsync(CreateImageCommand image, CancellationToken cancellationToken)
    {
        var createImageCommandValidator = imagesValidator.CreateImageCommandValidator;

        await createImageCommandValidator.ValidateAndThrowAsync(image, cancellationToken);

        return await imagesService.CreateImageAsync(image, cancellationToken);
    }

    public async Task<Guid> DeleteImageAsync(DeleteImageCommand image, CancellationToken cancellationToken)
    {
        var deleteImageCommandValidator = imagesValidator.DeleteImageCommandValidator;

        await deleteImageCommandValidator.ValidateAndThrowAsync(image, cancellationToken);

        return await imagesService.DeleteImageAsync(image, cancellationToken);
    }

    public async Task<Guid> UpdateImageAsync(UpdateImageCommand image, CancellationToken cancellationToken)
    {
        var updateImageCommandValidator = imagesValidator.UpdateImageCommandValidator;

        await updateImageCommandValidator.ValidateAndThrowAsync(image, cancellationToken);

        return await imagesService.UpdateImageAsync(image, cancellationToken);
    }
}
