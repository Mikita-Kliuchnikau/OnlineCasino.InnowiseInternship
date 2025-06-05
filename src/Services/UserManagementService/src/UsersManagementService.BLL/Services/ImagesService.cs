using Mapster;
using Microsoft.Extensions.Logging;
using UsersManagementService.BLL.Attributes;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.BLL.Validators.ImagesValidators;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Interfaces.Repositories;
using UsersManagementService.DAL.Interfaces.Services;

namespace UsersManagementService.BLL.Services
{
    public class ImagesService(IImagesRepository imagesRepository, IAzureBlobService blobService, ILogger<ImagesService> logger) : IImagesService
    {
        [Validate(typeof(ImageModelValidator))]
        public virtual async Task<Guid> CreateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
        {
            logger.LogInformation(
                "Processing request {RequestName}, {@Model}",
                nameof(CreateImageAsync),
                image);

            if ( await imagesRepository.DoesImageExistAsync(image.Id, cancellationToken))
            {
                logger.LogError(
                    "Request {RequestName} failed. Image with id {@id} aready exists",
                    nameof(CreateImageAsync),
                    image.Id);
                throw new InvalidOperationException($"Image with id {image.Id} already exists.");
            }

            var imageUrl = await blobService.UploadImageAsync(
                image.Stream,
                image.Id.ToString("N"),
                image.ContentType,
                cancellationToken);

            logger.LogInformation(
                "Complited request {RequestName} blob storage with result {@Result}",
                nameof(blobService.UploadImageAsync),
                imageUrl);

            var imageEntity = image.Adapt<ImageEntity>();
            imageEntity.ImageUrl = imageUrl;

            var result = await imagesRepository.CreateAsync(imageEntity, cancellationToken);

            logger.LogInformation(
                "Complited request {RequestName} with result {@Result}",
                nameof(CreateImageAsync),
                result);

            return result;
        }

        [Validate(typeof(ImageIdValidator))]
        public virtual async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation(
                "Processing request {RequestName}, {@Model}",
                nameof(DeleteImageAsync),
                id);

            var result = await imagesRepository.DeleteAsync(id, cancellationToken);

            logger.LogInformation(
                "Complited request {RequestName} with result {@Result}",
                nameof(DeleteImageAsync),
                result);

            return result;
        }
    }
}