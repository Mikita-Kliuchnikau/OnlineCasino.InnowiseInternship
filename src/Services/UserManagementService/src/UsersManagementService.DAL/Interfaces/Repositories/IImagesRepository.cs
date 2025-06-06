using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.DAL.Interfaces.Repositories;

public interface IImagesRepository
{
    Task<Guid> CreateAsync(ImageEntity image, CancellationToken cancellationToken = default);

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsImageUniqeAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> DoesImageExistAsync(Guid id, CancellationToken cancellationToken = default);
}