using Mapster;
using Microsoft.Extensions.Logging;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Services
{
    public class ImagesService(IImagesRepository imagesRepository, ILogger<ImagesService> logger) : IImagesService
    {
        public async Task<Guid> CreateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
        {
            var imageEntity = image.Adapt<ImageEntity>();
            
            logger.LogInformation(
                "Processing request {RequestName}, {@Model}",
                nameof(CreateImageAsync),
                imageEntity);

            var result = await imagesRepository.CreateAsync(imageEntity, cancellationToken);

            logger.LogInformation(
                "Complited request {RequestName} with result {@Result}",
                nameof(CreateImageAsync),
                result);

            return result;
        }

        public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
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

        public async Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
        {
            var imageEntity = image.Adapt<ImageEntity>();
            
            logger.LogInformation(
                "Processing request {RequestName}, {@Model}",
                nameof(UpdateImageAsync),
                imageEntity);

            var result = await imagesRepository.UpdateAsync(imageEntity, cancellationToken);

            logger.LogInformation(
                "Complited request {RequestName} with result {@Result}",
                nameof(UpdateImageAsync),
                result);

            return result;
        }
    }
}