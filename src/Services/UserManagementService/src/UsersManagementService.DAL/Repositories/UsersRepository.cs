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
        if (!await ExistsAsync(user.Id, cancellationToken))
        {
            throw new NotFoundException(nameof(user), user.Id);
        }

        await context.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Username, user.Username)
                .SetProperty(u => u.Email, user.Email)
                .SetProperty(u => u.Balance, user.Balance)
                .SetProperty(u => u.VerificationStatus, user.VerificationStatus)
                .SetProperty(u => u.FirstName, user.FirstName)
                .SetProperty(u => u.SecondName, user.SecondName)
                .SetProperty(u => u.LastName, user.LastName)
                .SetProperty(u => u.BirthDate, user.BirthDate)
                .SetProperty(u => u.PassportNumber, user.PassportNumber)
                .SetProperty(u => u.IdentificationNumber, user.IdentificationNumber), 
                cancellationToken: cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<bool> TryChangeBalance(
        Guid id,
        decimal newBalance,
        CancellationToken cancellationToken = default)
    {
        var result = await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Balance, newBalance),
                cancellationToken: cancellationToken);

        return result != 0;
    }

    public async Task<Guid> BanAsync(
        Guid id,
        bool isBanned,
        CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException(nameof(user), id);
        }
        await context.Users.ExecuteUpdateAsync(u => u
            .SetProperty(u => u.IsBanned, isBanned),
            cancellationToken: cancellationToken);
        return user.Id;
    }

    public async Task<bool> IsUniqueAsync(
        string authId, 
        string username, 
        string email, 
        CancellationToken cancellationToken = default)
    {
        return !await context.Users.AnyAsync(u =>
            u.AuthId.Equals(authId) ||
            u.Username.Equals(username) ||
            u.Email.Equals(email), 
            cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.AnyAsync(u => u.Id == id, cancellationToken);
    }
}