using UsersManagementService.DAL.Entites;

namespace UsersManagementService.DAL.Interfaces;

public interface IImagesRepository
{
    Task<Guid> CreateAsync(ImageEntity user, CancellationToken cancellationToken);

    Task<Guid> UpdateAsync(ImageEntity user, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
