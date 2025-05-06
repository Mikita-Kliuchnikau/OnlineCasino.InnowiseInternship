using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using UsersManagementService.DAL.Context;
using UsersManagementService.DAL.Entites;
using UsersManagementService.DAL.Interfaces;

namespace UsersManagementService.DAL.Repositories;

public class UsersRepository(UsersDbContext context) : IUsersRepository
{
    public async Task<Guid> CreateAsync(
        UserEntity user, 
        CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<Guid> DeleteAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        context.Remove(user);

        await context.SaveChangesAsync(cancellationToken);

        return id;
    }

    public async Task<List<UserEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users
                .AsNoTracking()
                .Include(u => u.Images)
                .ToListAsync(cancellationToken);
    }

    public async Task<UserEntity> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var user = await context.Users
                .AsNoTracking()
                .Include(u => u.Images)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

        return user;
    }

    public async Task<PagedUsersProjection> GetPagedAsync(
        PagedUsersFilter pagedUsersRequest, 
        CancellationToken cancellationToken = default)
    {
        var users = await context.Users
            .AsNoTracking()
            .Include(u => u.Images)
            .Skip((pagedUsersRequest.PageNumber - 1) * pagedUsersRequest.PageSize)
            .Take(pagedUsersRequest.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedUsersProjection
        {
            PageNumber = pagedUsersRequest.PageNumber,
            TotalCount = context.Users.AsNoTracking().Count(),
            Users = users
        };
    }

    public async Task<Guid> UpdateAsync(
        UserEntity user, 
        CancellationToken cancellationToken = default)
    {
        context.Users.Update(user);
        
        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
