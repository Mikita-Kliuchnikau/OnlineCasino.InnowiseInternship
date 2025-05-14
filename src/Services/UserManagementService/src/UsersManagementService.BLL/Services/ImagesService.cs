using Mapster;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Services
{
    public class ImagesService(IImagesRepository imagesRepository) : IImagesService
    {
        public async Task<Guid> CreateImageAsync(CreateImageModel image, CancellationToken cancellationToken = default)
        {
            var imageEntity = image.Adapt<ImageEntity>();
            return await imagesRepository.CreateAsync(imageEntity, cancellationToken);
        }
        
        public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await imagesRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<Guid> UpdateImageAsync(UpdateImageModel image, CancellationToken cancellationToken = default)
        {
            var imageEntity = image.Adapt<ImageEntity>();
            return await imagesRepository.UpdateAsync(imageEntity, cancellationToken);
        }
    }
}