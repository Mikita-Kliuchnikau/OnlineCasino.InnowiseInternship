using FluentValidation;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;

namespace UsersManagementService.BLL.Services.Decorators;

public class ImagesServiceValidationDecorator(IImagesService imagesService, IImagesValidator imagesValidator) : IImagesService
{
    public async Task<Guid> CreateImageAsync(CreateImageModel image, CancellationToken cancellationToken = default)
    {
        var createImageModelValidator = imagesValidator.CreateImageModelValidator;

        await createImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);

        return await imagesService.CreateImageAsync(image, cancellationToken);
    }

    public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleteImageModelValidator = imagesValidator.DeleteImageValidator;

        await deleteImageModelValidator.ValidateAndThrowAsync(id, cancellationToken);

        return await imagesService.DeleteImageAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateImageAsync(UpdateImageModel image, CancellationToken cancellationToken = default)
    {
        var updateImageModelValidator = imagesValidator.UpdateImageModelValidator;

        await updateImageModelValidator.ValidateAndThrowAsync(image, cancellationToken);

        return await imagesService.UpdateImageAsync(image, cancellationToken);
    }
}
