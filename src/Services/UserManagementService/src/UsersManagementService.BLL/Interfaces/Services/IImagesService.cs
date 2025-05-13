using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;

namespace UsersManagementService.BLL.Interfaces.Services;

public interface IImagesService
{
    Task<Guid> CreateImageAsync(CreateImageCommand image, CancellationToken cancellationToken);

    Task<Guid> UpdateImageAsync(UpdateImageCommand image, CancellationToken cancellationToken);

    Task<Guid> DeleteImageAsync(DeleteImageCommand image, CancellationToken cancellationToken);
}