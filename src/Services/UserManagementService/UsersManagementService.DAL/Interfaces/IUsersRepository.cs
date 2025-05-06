using UsersManagementService.DAL.Entites;

namespace UsersManagementService.DAL.Interfaces;

public interface IUsersRepository
{
    Task<Guid> CreateAsync(UserEntity user, CancellationToken cancellationToken);

    Task<List<UserEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<PagedUsersProjection> GetPagedAsync(PagedUsersFilter pagedUsersFilter, CancellationToken cancellationToken);

    Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Guid> UpdateAsync(UserEntity user, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
