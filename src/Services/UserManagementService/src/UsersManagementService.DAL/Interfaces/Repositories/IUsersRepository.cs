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

    Task<bool> IsUserUniqeAsync(
        Guid? id = null,
        Guid? authId = null,
        string? username = null,
        string? email = null,
        CancellationToken cancellationToken = default);

    Task<bool> DoesUserExistAsync(Guid id, CancellationToken cancellationToken = default);
}