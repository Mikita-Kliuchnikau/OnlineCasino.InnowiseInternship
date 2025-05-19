using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;

namespace UsersManagementService.BLL.Interfaces.Services;

public interface IImagesService
{
    Task<Guid> CreateImageAsync(CreateImageModel image, CancellationToken cancellationToken = default);

    Task<Guid> UpdateImageAsync(UpdateImageModel image, CancellationToken cancellationToken = default);

    Task<Guid> DeleteImageAsync(Guid id, CancellationToken cancellationToken = default);
}