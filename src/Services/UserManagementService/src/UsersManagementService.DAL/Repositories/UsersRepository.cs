using Microsoft.EntityFrameworkCore;
using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Context;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Entites.Dto;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.DAL.Repositories;

public class UsersRepository(UsersDbContext context) : IUsersRepository
{
    public async Task<Guid> CreateAsync(
        UserEntity user,
        CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<Guid> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(user), id);
        }

        context.Remove<UserEntity>(user);

        await context.SaveChangesAsync(cancellationToken);

        return id;
    }

    public async Task<UserEntity> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var user = await context.Users
                .AsNoTracking()
                .Include(u => u.Images)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(user), id);
        }

        return user;
    }

    public async Task<PagedUsersProjection> GetPagedAsync(
        PagedUsersFilter pagedUsersFilter,
        CancellationToken cancellationToken = default)
    {
        var users = await context.Users
            .AsNoTracking()
            .Include(u => u.Images)
            .Skip((pagedUsersFilter.PageNumber - 1) * pagedUsersFilter.PageSize)
            .Take(pagedUsersFilter.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedUsersProjection
        {
            PageNumber = pagedUsersFilter.PageNumber,
            TotalCount = await context.Users.AsNoTracking().CountAsync(cancellationToken),
            Users = users
        };
    }

    public async Task<Guid> UpdateAsync(
        UserEntity user,
        CancellationToken cancellationToken = default)
    {
        if (await DoesUserExistAsync(user.Id, cancellationToken) is false)
        {
            throw new NotFoundException(nameof(user), user.Id);
        }

        context.Users.Update(user);

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<bool> IsUserUniqeAsync(
        Guid? id = null, 
        Guid? authId = null, 
        string? username = null, 
        string? email = null, 
        CancellationToken cancellationToken = default)
    {
        return !await context.Users.AnyAsync(u =>
            (id != null && u.Id == id) ||
            (authId != null && u.AuthId == authId) ||
            (username != null && u.Username == username) ||
            (email != null && u.Email == email), 
            cancellationToken);
    }

    public async Task<bool> DoesUserExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.AnyAsync(u => u.Id == id, cancellationToken);
    }
}