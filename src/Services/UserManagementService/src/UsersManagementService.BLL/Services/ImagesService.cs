using Mapster;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Services
{
    public class ImagesService(IImagesRepository imagesRepository) : IImagesService
    {
        public async Task<Guid> CreateImageAsync(CreateImageCommand user, CancellationToken cancellationToken = default)
        {
            var imageEntity = user.Adapt<ImageEntity>();
            return await imagesRepository.CreateAsync(imageEntity, cancellationToken);
        }

        public async Task<Guid> DeleteImageAsync(DeleteImageCommand user, CancellationToken cancellationToken = default)
        {
            return await imagesRepository.DeleteAsync(user.Id, cancellationToken);
        }

        public async Task<Guid> UpdateImageAsync(UpdateImageCommand user, CancellationToken cancellationToken = default)
        {
            var imageEntity = user.Adapt<ImageEntity>();
            return await imagesRepository.UpdateAsync(imageEntity, cancellationToken);
        }
    }
}