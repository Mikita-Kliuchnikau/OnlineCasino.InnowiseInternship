using UsersManagementService.BLL.Models.Image;

namespace UsersManagementService.BLL.Interfaces.Services;

public interface IImagesService
{
    Task<Guid> CreateImageAsync(ImageModel image, CancellationToken cancellationToken = default);

    Task<Guid> UpdateImageAsync(ImageModel image, CancellationToken cancellationToken = default);

    Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default);
}