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
                "Processing request {RequestName}, {@Model}, {@DateTime}",
                nameof(CreateImageAsync),
                imageEntity,
                DateTime.UtcNow);

            var result = await imagesRepository.CreateAsync(imageEntity, cancellationToken);

            logger.LogInformation(
                "Complited request {RequestName} with result {@Result}, {@DateTime}",
                nameof(CreateImageAsync),
                result,
                DateTime.UtcNow);

            return result;
        }

        public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation(
                "Processing request {RequestName}, {@Model}, {@DateTime}",
                nameof(DeleteImageAsync),
                id,
                DateTime.UtcNow);

            var result = await imagesRepository.DeleteAsync(id, cancellationToken);

            logger.LogInformation(
                "Complited request {RequestName} with result {@Result}, {@DateTime}",
                nameof(DeleteImageAsync),
                result,
                DateTime.UtcNow);

            return result;
        }

        public async Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
        {
            var imageEntity = image.Adapt<ImageEntity>();
            
            logger.LogInformation(
                "Processing request {RequestName}, {@Model}, {@DateTime}",
                nameof(UpdateImageAsync),
                imageEntity,
                DateTime.UtcNow);

            var result = await imagesRepository.UpdateAsync(imageEntity, cancellationToken);

            logger.LogInformation(
                "Complited request {RequestName} with result {@Result}, {@DateTime}",
                nameof(UpdateImageAsync),
                result,
                DateTime.UtcNow);

            return result;
        }
    }
}