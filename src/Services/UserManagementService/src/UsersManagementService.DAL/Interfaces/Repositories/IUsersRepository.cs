using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.Dto;

namespace UsersManagementService.DAL.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<Guid> CreateAsync(UserEntity user, CancellationToken cancellationToken = default);

    Task<PagedUsersProjection> GetPagedAsync(PagedUsersFilter pagedUsersFilter, CancellationToken cancellationToken = default);

    Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Guid> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default);

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Guid> BanAsync(Guid id, bool isBanned, CancellationToken cancellationToken = default);

    Task<bool> IsUniqueAsync(
        string authId,
        string username,
        string email,
        CancellationToken cancellationToken = default);

    Task<bool> DoesExistAsync(Guid id, CancellationToken cancellationToken = default);
}