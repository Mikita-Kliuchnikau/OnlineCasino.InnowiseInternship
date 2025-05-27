using Mapster;
using Microsoft.Extensions.Logging;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Interfaces.Repositories;
using static UsersManagementService.Common.Constants.LoggingMessages;

namespace UsersManagementService.BLL.Services
{
    public class ImagesService(IImagesRepository imagesRepository, ILogger<ImagesService> logger) : IImagesService
    {
        public async Task<Guid> CreateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
        {
            logger.LogInformation(string.Format(
                RequestStartingMessage,
                nameof(CreateImageAsync),
                DateTime.UtcNow));

            var imageEntity = image.Adapt<ImageEntity>();
            var result = Guid.Empty;

            try
            {
                result = await imagesRepository.CreateAsync(imageEntity, cancellationToken);
            }
            catch(Exception ex)
            {
                logger.LogError(string.Format(
                    RequestFailedMessage,
                    nameof(CreateImageAsync),
                    ex.Message,
                    DateTime.UtcNow));
                throw;
            }

            logger.LogInformation(string.Format(
                RequestSucceededMessage,
                nameof(CreateImageAsync),
                result,
                DateTime.UtcNow));

            return result;
        }

        public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation(string.Format(
                RequestStartingMessage,
                nameof(DeleteImageAsync),
                DateTime.UtcNow));

            var result = Guid.Empty;

            try
            {
                result = await imagesRepository.DeleteAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(
                    RequestFailedMessage,
                    nameof(DeleteImageAsync),
                    ex.Message,
                    DateTime.UtcNow));
                throw;
            }

            logger.LogInformation(string.Format(
                RequestSucceededMessage,
                nameof(DeleteImageAsync),
                result,
                DateTime.UtcNow));

            return result;
        }

        public async Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default)
        {
            logger.LogInformation(string.Format(
                RequestStartingMessage,
                nameof(UpdateImageAsync),
                DateTime.UtcNow));

            var imageEntity = image.Adapt<ImageEntity>();
            var result = Guid.Empty;

            try
            {
                result = await imagesRepository.UpdateAsync(imageEntity, cancellationToken);
            }
            catch(Exception ex)
            {
                logger.LogError(string.Format(
                    RequestFailedMessage,
                    nameof(UpdateImageAsync),
                    ex.Message,
                    DateTime.UtcNow));
                throw;
            }

            logger.LogInformation(string.Format(
                RequestSucceededMessage,
                nameof(UpdateImageAsync),
                result,
                DateTime.UtcNow));

            return result;
        }
    }
}