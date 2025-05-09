using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.DAL.Interfaces.Repositories;

public interface IImagesRepository
{
    Task<Guid> CreateAsync(ImageEntity image, CancellationToken cancellationToken);

    Task<Guid> UpdateAsync(ImageEntity image, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken);
}